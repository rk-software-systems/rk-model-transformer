using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Extensions;
using RKSoftware.Packages.ModelTransformer.Models;

namespace RKSoftware.Packages.ModelTransformer.Builders;

internal class ExtensionMethodCodeBuilder
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
                    propBuilder.CreateVariableByDefaultMappingMethod(mapping);
                }
                else
                {
                    propBuilder.CreateVariableByRequiredMappingMethod(mapping);
                }
            }

            if (mapping.IsConstructorParam)
            {
                propBuilder.SetVariableInConstructor(mapping);
            }
            else
            {
                propBuilder.SetVariablePostConstructor(mapping);
            }

            if (!mapping.IsReadonly && !isIgnored)
            {
                propBuilder.SetVariable(mapping);
            }

            if (!isIgnored)
            {
                if (mappingCode != null)
                {
                    propBuilder.CreateDefaultMappingMethod(mapping, attr, mappingCode);
                    propBuilder.CreateOptionalMappingMethodToBeImplemented(mapping, attr);
                }
                else
                {
                    propBuilder.CreateRequiredMappingMethodToBeImplemented(mapping, attr);
                }
            }
            _properties.Add(propBuilder);
        }
    }

    public string Generate()
    {
        var variableCreationCode = string.Empty;
        var methodsCode = string.Empty;
        var constructorVariableMappingCode = string.Empty;
        var postConstructorVariableMappingCode = string.Empty;
        var variableMappingCode = string.Empty;

        if (_properties.Count > 0)
        {
            variableCreationCode = CreateVariableCreateionCode();

            constructorVariableMappingCode = CreateConstructorVariableMappingCode();

            postConstructorVariableMappingCode = CreatePostConstructorVariableMappingCode();

            variableMappingCode = CreateVariableMappingCode();

            methodsCode = CreateMethodsCode();
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
{variableCreationCode}
{Constants.Indent__3}if (target == null)
{Constants.Indent__3}{{
{Constants.Indent___4}target = new {targetTypeName} ({constructorVariableMappingCode})
{Constants.Indent___4}{{
{postConstructorVariableMappingCode}
{Constants.Indent___4}}};
{Constants.Indent__3}}}
{Constants.Indent__3}else
{Constants.Indent__3}{{
{variableMappingCode}
{Constants.Indent__3}}}
{Constants.Indent__3}return target;
{Constants.Indent_2}}}
{methodsCode}
{Constants.Indent_2}#endregion
";
        return str;
    }

    private string CreateVariableCreateionCode()
    {
        var variableCreations = _properties
                .Select(x => x.VariableCreationCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return variableCreations.Count > 0
            ? $"{Constants.NewLine}{string.Join($"{Constants.NewLine}{Constants.NewLine}", variableCreations)}{Constants.NewLine}"
            : string.Empty;
    }

    private string CreateConstructorVariableMappingCode()
    {
        var constructorVariableMappings = _properties
                .Select(x => x.ConstructorVariableMappingCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return constructorVariableMappings.Count > 0 ?
             $"{Constants.NewLine}{string.Join($",{Constants.NewLine}", constructorVariableMappings)}" :
             string.Empty;
    }

    private string CreatePostConstructorVariableMappingCode()
    {
        var postConstructorVariableMappings = _properties
                .Select(x => x.PostConstructorVariableMappingCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return postConstructorVariableMappings.Count > 0 ?
             string.Join($",{Constants.NewLine}", postConstructorVariableMappings) :
             string.Empty;
    }

    private string CreateVariableMappingCode()
    {
        var variableMappings = _properties
                .Select(x => x.VariableMappingCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return variableMappings.Count > 0 ?
             string.Join($"{Constants.NewLine}", variableMappings) :
             string.Empty;

    }

    private string CreateMethodsCode()
    {
        var methods = _properties
                .Select(x => x.MethodCode)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

        return methods.Count > 0 ?
            $"{Constants.NewLine}{string.Join($"{Constants.NewLine}{Constants.NewLine}", methods)}{Constants.NewLine}" :
            string.Empty;
    }
}
