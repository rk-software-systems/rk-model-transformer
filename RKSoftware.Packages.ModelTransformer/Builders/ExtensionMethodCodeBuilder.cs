using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Extensions;
using RKSoftware.Packages.ModelTransformer.Models;

namespace RKSoftware.Packages.ModelTransformer.Builders;

internal sealed class ExtensionMethodCodeBuilder
{
    private readonly AttributeDataModel _attr;

    private readonly Dictionary<string, List<AttributeDataModel>> _typeRegistrations;

    private readonly List<PropertyMappingCodeBuilder> _properties;

    public ExtensionMethodCodeBuilder(AttributeDataModel attr, Dictionary<string, List<AttributeDataModel>> typeRegistrations)
    {
        _attr = attr;
        _typeRegistrations = typeRegistrations;
        _properties = new List<PropertyMappingCodeBuilder>(attr.TargetProperties.Count);

        foreach (var targetProp in attr.TargetProperties)
        {
            var propBuilder = new PropertyMappingCodeBuilder();

            IPropertySymbol? sourceProp = null;
            string? mappingCode = null;

            var isIgnored = attr.IgnoredProperties.TryGetValue(targetProp.Key, out _);

            var mapping = new PropertyMappingModel(targetProp.Value, attr, isIgnored);

            if (!isIgnored)
            {
                if (attr.SourceProperties.TryGetValue(targetProp.Key, out sourceProp))
                {
                    if (sourceProp.CanNotConvertType(targetProp.Value))
                    {
                        if (_typeRegistrations.TryGetValue(sourceProp.Type.OriginalDefinition.ToDisplayString(), out var complexTargets) &&
                            complexTargets.Any(x => SymbolEqualityComparer.Default.Equals(x.Target, targetProp.Value.Type)))
                        {
                            mappingCode = $"source.{sourceProp.Name}{(sourceProp.IsNullable() ? "?" : "")}.Transform(({mapping.PropertyType.OriginalDefinition.ToDisplayString()}?)null)";
                        }
                        else
                        {
                            var sourceArgumentType = sourceProp.GetGenericArgumentType();
                            var targetArgumentType = targetProp.Value.GetGenericArgumentType();
                            if (sourceArgumentType != null &&
                                targetArgumentType != null &&
                                (targetArgumentType.IsNullable() || !sourceArgumentType.IsNullable()))
                            {
                                string? str = null;

                                if (_typeRegistrations.TryGetValue(sourceArgumentType.OriginalDefinition.ToDisplayString(), out var complexArgumentTargets) &&
                                    complexArgumentTargets.Any(x => SymbolEqualityComparer.Default.Equals(x.Target, targetArgumentType)))
                                {
                                    if (mapping.PropertyType.IsGenericInterfaceConstructable() || mapping.PropertyType.IsArrayType())
                                    {
                                        str = $"[.. source.{sourceProp.Name}.Select(x => x{(sourceArgumentType.IsNullable() ? "?" : "")}.Transform(({targetArgumentType.OriginalDefinition.ToDisplayString()}?)null))]";
                                    }
                                    else if (mapping.PropertyType.GetEnumerableParameterInConstructor() is INamedTypeSymbol nt)
                                    {
                                        str = $"source.{sourceProp.Name}.Select(x => x{(sourceArgumentType.IsNullable() ? "?" : "")}.Transform(({targetArgumentType.OriginalDefinition.ToDisplayString()}?)null))";
                                        if (nt.IsEnumerableInterfaceSpecial() && !nt.IsListInterfaceSpecial() && !nt.IsCollectionInterfaceSpecial())
                                        {

                                        }
                                        else if (nt.IsCollectionInterfaceSpecial() || nt.IsArrayType())
                                        {
                                            str = $"{str}.ToArray()";
                                        }
                                        else
                                        {
                                            str = $"{str}.ToList()";
                                        }
                                        str = $"new ({str})";
                                    }
                                    else if (mapping.PropertyType.IsDictionaryType())
                                    {
                                        str = $"source.{sourceProp.Name}.ToDictionary(x => x.Key, y => y.Value{(sourceArgumentType.IsNullable() ? "?" : "")}.Transform(({targetArgumentType.OriginalDefinition.ToDisplayString()}?)null))";
                                    }
                                }
                                else if (sourceArgumentType.IsPrimitiveOrString() &&
                                        targetArgumentType.IsPrimitiveOrString() &&
                                        SymbolEqualityComparer.Default.Equals(sourceArgumentType, targetArgumentType))
                                {
                                    if (mapping.PropertyType is INamedTypeSymbol nt && nt.Constructors.Any(c => c.Parameters.Length > 0))
                                    {
                                        str = $"source.{sourceProp.Name}[..]";
                                    }
                                    else if (mapping.PropertyType.IsGenericInterfaceConstructable())
                                    {
                                        if (mapping.PropertyType is INamedTypeSymbol nts &&
                                            nts.IsListInterfaceSpecial())
                                        {
                                            str = $"source.{sourceProp.Name}.ToList()";
                                        }
                                        else
                                        {
                                            str = $"source.{sourceProp.Name}.ToArray()";
                                        }
                                    }
                                    else if (mapping.PropertyType.IsArrayType())
                                    {
                                        str = $"source.{sourceProp.Name}[..]";
                                    }
                                }

                                if (str != null)
                                {
                                    mappingCode = sourceProp.IsNullable() ? $"source.{sourceProp.Name} != null ? {str} : default" : str;
                                }
                            }
                        }
                    }
                    else
                    {
                        mappingCode = $"source.{sourceProp.Name}";
                    }
                }
            }

            if (!isIgnored)
            {
                if (mappingCode != null)
                {
                    propBuilder.SetVariableDefaultMappingMethodCode(mapping);
                }
                else
                {
                    propBuilder.SetVariableRequiredMappingMethodCode(mapping);
                }
            }

            if (mapping.IsConstructorParam)
            {
                propBuilder.SetVariableMappingCodeInConstructor(mapping);
            }
            else
            {
                propBuilder.SetVariableMappingCodeAfterConstructor(mapping);
            }

            if (!mapping.IsReadonly && !isIgnored)
            {
                propBuilder.SetVariableMappingCode(mapping);
            }

            if (!isIgnored)
            {
                if (mappingCode != null)
                {
                    propBuilder.SetDefaultMappingMethodCode(mapping, attr, mappingCode);
                    propBuilder.AddOptionalMappingMethodCode(mapping, attr);
                }
                else
                {
                    propBuilder.SetRequiredMappingMethodCode(mapping, attr);
                }
            }
            _properties.Add(propBuilder);
        }
    }

    public string Generate()
    {
        var variableMappingMethodCode = string.Empty;        
        var variableMappingCodeInConstructor = string.Empty;
        var variableMappingCodeAfterConstructor = string.Empty;

        var variableMappingCode = string.Empty;
        var mappingMethodCode = string.Empty;

        if (_properties.Count > 0)
        {
            variableMappingMethodCode = CreateVariableMappingMethodCode();

            variableMappingCodeInConstructor = CreateVariableMappingCodeInConstructor();

            variableMappingCodeAfterConstructor = CreateVariableMappingCodeAfterConstructor();

            variableMappingCode = CreateVariableMappingCode();

            mappingMethodCode = CreateMappingMethodCode();
        }

        var targetName = _attr.Target.Name;
        var targetTypeName = _attr.Target.ToDisplayString();
        var sourceTypeName = _attr.Source.ToDisplayString();

        var str = $@"
{Constants.Indent_2}#region to {targetName}

{Constants.Indent_2}public static {targetTypeName} Transform (this {sourceTypeName} source, {targetTypeName}? target = null)
{Constants.Indent_2}{{
{Constants.Indent__3}if (source == null) 
{Constants.Indent__3}{{
{Constants.Indent___4}throw new System.ArgumentNullException(nameof(source));
{Constants.Indent__3}}}
{variableMappingMethodCode}
{Constants.Indent__3}if (target == null)
{Constants.Indent__3}{{
{Constants.Indent___4}target = new {targetTypeName} ({variableMappingCodeInConstructor})
{Constants.Indent___4}{{
{variableMappingCodeAfterConstructor}
{Constants.Indent___4}}};
{Constants.Indent__3}}}
{Constants.Indent__3}else
{Constants.Indent__3}{{
{variableMappingCode}
{Constants.Indent__3}}}
{Constants.Indent__3}return target;
{Constants.Indent_2}}}
{mappingMethodCode}
{Constants.Indent_2}#endregion
";
        return str;
    }

    private string CreateVariableMappingMethodCode()
    {
        var code = _properties
                .Select(x => x.VariableMappingMethodCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return code.Count > 0
            ? $"{Constants.NewLine}{string.Join($"{Constants.NewLine}{Constants.NewLine}", code)}{Constants.NewLine}"
            : string.Empty;
    }

    private string CreateVariableMappingCodeInConstructor()
    {
        var code = _properties
                .Select(x => x.VariableMappingCodeInConstructor)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return code.Count > 0 ?
             $"{Constants.NewLine}{string.Join($",{Constants.NewLine}", code)}" :
             string.Empty;
    }

    private string CreateVariableMappingCodeAfterConstructor()
    {
        var code = _properties
                .Select(x => x.VariableMappingCodeAfterConstructor)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return code.Count > 0 ?
             string.Join($",{Constants.NewLine}", code) :
             string.Empty;
    }

    private string CreateVariableMappingCode()
    {
        var code = _properties
                .Select(x => x.VariableMappingCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return code.Count > 0 ?
             string.Join($"{Constants.NewLine}", code) :
             string.Empty;

    }

    private string CreateMappingMethodCode()
    {
        var code = _properties
                .Select(x => x.MappingMethodCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return code.Count > 0 ?
            $"{Constants.NewLine}{string.Join($"{Constants.NewLine}{Constants.NewLine}", code)}{Constants.NewLine}" :
            string.Empty;
    }
}
