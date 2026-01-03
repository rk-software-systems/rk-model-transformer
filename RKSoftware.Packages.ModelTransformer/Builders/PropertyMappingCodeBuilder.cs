using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Extensions;
using RKSoftware.Packages.ModelTransformer.Models;

namespace RKSoftware.Packages.ModelTransformer.Builders;

internal sealed class PropertyMappingCodeBuilder
{
    private string _variableMappingMethodCode = string.Empty;
    private string _variableMappingCodeInConstructor = string.Empty;
    private string _variableMappingCodeAfterConstructor = string.Empty;
    private string _variableMappingCode = string.Empty;
    private string _mappingMethodCode = string.Empty;
    private string _mappingCode = string.Empty;


    public string VariableMappingMethodCode => _variableMappingMethodCode;

    public string VariableMappingCodeInConstructor => _variableMappingCodeInConstructor;

    public string VariableMappingCodeAfterConstructor => _variableMappingCodeAfterConstructor;

    public string VariableMappingCode => _variableMappingCode;

    public string MappingMethodCode => _mappingMethodCode;

    public bool HasMappingCode => !string.IsNullOrEmpty(_mappingCode);


    public void SetVariableDefaultMappingMethodCode(PropertyMappingModel mapping)
    {
        _variableMappingMethodCode =
@$"{Constants.Indent__3}var {mapping.VariableName} = {mapping.DefaultMethodName}(source);
{Constants.Indent__3}{mapping.MethodName}(source, ref {mapping.VariableName});";
    }

    public void SetVariableRequiredMappingMethodCode(PropertyMappingModel mapping)
    {
        _variableMappingMethodCode =
@$"{Constants.Indent__3}var {mapping.VariableName} = {mapping.MethodName}(source);";
    }

    public void SetVariableMappingCodeInConstructor(PropertyMappingModel mapping)
    {
        _variableMappingCodeInConstructor =
$@"{Constants.Indent____5}{mapping.PropertyName} : {mapping.VariableName}";
    }

    public void SetVariableMappingCodeAfterConstructor(PropertyMappingModel mapping)
    {
        _variableMappingCodeAfterConstructor =
$@"{Constants.Indent____5}{mapping.PropertyName} = {mapping.VariableName}";
    }

    public void SetVariableMappingCode(PropertyMappingModel mapping)
    {
        _variableMappingCode =
$@"{Constants.Indent___4}target.{mapping.PropertyName} = {mapping.VariableName};";
    }

    public void SetDefaultMappingMethodCode(PropertyMappingModel mapping, AttributeDataModel attr)
    {
        _mappingMethodCode =
$@"{Constants.Indent_2}private static {mapping.PropertyType.ToDisplayString()} {mapping.DefaultMethodName}({attr.Source.ToDisplayString()} source)
{Constants.Indent_2}{{
{Constants.Indent__3}return {_mappingCode};
{Constants.Indent_2}}}";
    }

    public void AddOptionalMappingMethodCode(PropertyMappingModel mapping, AttributeDataModel attr)
    {
        var code =
 $@"{Constants.Indent_2}static partial void {mapping.MethodName}({attr.Source.ToDisplayString()} source, ref {mapping.PropertyType.ToDisplayString()} target);";
        _mappingMethodCode = $"{_mappingMethodCode}{Constants.NewLine}{code}";
    }

    public void SetRequiredMappingMethodCode(PropertyMappingModel mapping, AttributeDataModel attr)
    {
        _mappingMethodCode =
$@"{Constants.Indent_2}private static partial {mapping.PropertyType.ToDisplayString()} {mapping.MethodName}({attr.Source.ToDisplayString()} source);";
    }

    public void SetComplexTypeMappingCode(PropertyMappingModel mapping, IPropertySymbol sourceProp)
    {
        _mappingCode =
$"source.{sourceProp.Name}{(sourceProp.IsNullable() ? "?" : "")}.Transform(({mapping.PropertyType.OriginalDefinition.ToDisplayString()}?)null)";
    }

    public void SetCollectionTypeMappingCode(IPropertySymbol sourceProp, string code)
    {
        _mappingCode =
sourceProp.IsNullable() ? $"source.{sourceProp.Name} != null ? {code} : {Constants.Default}" : code;
    }

    public void SetPrimitiveTypeMappingCode(IPropertySymbol sourceProp)
    {
        _mappingCode = $"source.{sourceProp.Name}";
    }

    public string GetEnumerableTypeCode(IPropertySymbol sourceProp, ITypeSymbol sourceArgumentType, ITypeSymbol targetArgumentType)
    {
        var showsNullCheck = sourceArgumentType.IsNullable() || targetArgumentType.IsNullable();
        return
$"source.{sourceProp.Name}.Select(x => x{(showsNullCheck ? "?" : "")}.Transform(({targetArgumentType.OriginalDefinition.ToDisplayString()}?)null))";
    }

    public string ApplyCloneCollectionTypeCode(string code)
    {
        return $"[.. {code}]";
    }

    public string ApplyCloneListTypeCode(string code)
    {
        return $"{code}.ToList()";
    }

    public string ApplyCreateNewTypeCode(string code)
    {
        return $"new ({code})";
    }

    public string GetCloneCollectionTypeCode(IPropertySymbol sourceProp)
    {
        return $"[.. source.{sourceProp.Name}]";
    }

    public string GetCreateNewTypeCode(IPropertySymbol sourceProp)
    {
        return $"new (source.{sourceProp.Name})";
    }
}
