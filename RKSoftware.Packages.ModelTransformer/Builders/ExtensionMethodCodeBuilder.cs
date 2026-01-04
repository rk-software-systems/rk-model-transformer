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

            var isIgnored = attr.IgnoredProperties.TryGetValue(targetProp.Key, out _);

            var mapping = new PropertyMappingModel(targetProp.Value, attr, isIgnored);

            if (!isIgnored)
            {
                if (attr.SourceProperties.TryGetValue(targetProp.Key, out sourceProp))
                {
                    // Type Mapping
                    if (sourceProp.CanNotConvertType(targetProp.Value))
                    {
                        // Complex Type Mapping
                        if (_typeRegistrations.TryGetValue(sourceProp.Type.OriginalDefinition.ToDisplayString(), out var complexTargets) &&
                            complexTargets.Any(x => SymbolEqualityComparer.Default.Equals(x.Target, targetProp.Value.Type)) &&
                            (targetProp.Value.IsNullable() || !sourceProp.IsNullable()))
                        {
                           
                            propBuilder.SetComplexTypeMappingCode(mapping, sourceProp);                            
                        }
                        // Collection Type Mapping
                        else
                        {                            
                            var sourceArgumentType = sourceProp.GetGenericArgumentType();
                            var targetArgumentType = targetProp.Value.GetGenericArgumentType();
                            if (sourceArgumentType != null &&
                                targetArgumentType != null &&
                               (targetArgumentType.IsNullable() || !sourceArgumentType.IsNullable()))
                            {
                                string? str = null;

                                // Check if the collection element types have a registered mapping
                                if (_typeRegistrations.TryGetValue(sourceArgumentType.OriginalDefinition.ToDisplayString(), out var complexArgumentTargets) &&
                                    complexArgumentTargets.Any(x => SymbolEqualityComparer.Default.Equals(x.Target, targetArgumentType)))
                                {
                                    if (mapping.PropertyType.IsGenericInterfaceConstructable() ||
                                        mapping.PropertyType.IsArrayType())
                                    {
                                        str = propBuilder.GetEnumerableTypeCode(sourceProp, sourceArgumentType, targetArgumentType);
                                        str = propBuilder.ApplyCloneCollectionTypeCode(str);
                                    }
                                    else if (mapping.PropertyType.GetEnumerableParameterInConstructor() is INamedTypeSymbol nt)
                                    {
                                        str = propBuilder.GetEnumerableTypeCode(sourceProp, sourceArgumentType, targetArgumentType);
                                        if (nt.IsEnumerableInterface())
                                        {

                                        }
                                        else if (nt.IsCollectionInterface() || nt.IsArrayType())
                                        {
                                            str = propBuilder.ApplyCloneCollectionTypeCode(str);
                                        }
                                        else
                                        {
                                            str = propBuilder.ApplyCloneListTypeCode(str);
                                        }
                                        str = propBuilder.ApplyCreateNewTypeCode(str);
                                    }
                                }
                                // Primitive type collection mapping
                                else if ((sourceArgumentType.IsPrimitiveOrString() || sourceArgumentType.IsStructure()) &&
                                         (targetArgumentType.IsPrimitiveOrString() || targetArgumentType.IsStructure()) &&
                                        SymbolEqualityComparer.Default.Equals(sourceArgumentType.GetNonNullable(), targetArgumentType.GetNonNullable()))
                                {
                                    if (mapping.PropertyType.IsGenericInterfaceConstructable() ||
                                        mapping.PropertyType.IsArrayType())
                                    {
                                        str = propBuilder.GetCloneCollectionTypeCode(sourceProp);
                                    }
                                    else if (mapping.PropertyType.GetEnumerableParameterInConstructor() is INamedTypeSymbol nt)
                                    {      
                                        if (!sourceArgumentType.IsNullable() && targetArgumentType.IsNullable())
                                        {
                                            str = propBuilder.GetCastCollectionTypeCode(sourceProp, targetArgumentType);
                                            if (nt.IsEnumerableInterface())
                                            {
                                            }
                                            else if (nt.IsCollectionInterface() || nt.IsArrayType())
                                            {
                                                str = propBuilder.ApplyCloneCollectionTypeCode(str);
                                            }
                                            else
                                            {
                                                str = propBuilder.ApplyCloneListTypeCode(str);
                                            }
                                        }
                                        else
                                        {
                                            str = propBuilder.GetSourcePropertyCode(sourceProp);
                                        }

                                        str = propBuilder.ApplyCreateNewTypeCode(str);                                        
                                    }
                                }

                                if (str != null)
                                {
                                    propBuilder.SetCollectionTypeMappingCode(sourceProp, str);
                                }
                            }
                        }
                    }
                    // Primitive Type Mapping
                    else
                    {
                        propBuilder.SetPrimitiveTypeMappingCode(sourceProp);
                    }
                }
            }

            if (!isIgnored)
            {
                if (propBuilder.HasMappingCode)
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
                if (propBuilder.HasMappingCode)
                {
                    propBuilder.SetDefaultMappingMethodCode(mapping, attr);
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
