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

    public void SetDefaultMappingMethodCode(PropertyMappingModel mapping, AttributeDataModel attr, string code)
    {
        _mappingMethodCode = 
$@"{Constants.Indent_2}private static {mapping.PropertyType.ToDisplayString()} {mapping.DefaultMethodName}({attr.Source.ToDisplayString()} source)
{Constants.Indent_2}{{
{Constants.Indent__3}return {code};
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
}
