using RKSoftware.Packages.ModelTransformer.Models;

namespace RKSoftware.Packages.ModelTransformer.Builders;

internal class PropertyMappingCodeBuilder
{
    private string _variableCreationCode = string.Empty;
    private string _constructorVariableMappingCode = string.Empty;
    private string _postConstructorVariableMappingCode = string.Empty;
    private string _variableMappingCode = string.Empty;
    private string _methodCode = string.Empty;


    public string VariableCreationCode => _variableCreationCode;

    public string ConstructorVariableMappingCode => _constructorVariableMappingCode;

    public string PostConstructorVariableMappingCode => _postConstructorVariableMappingCode;

    public string VariableMappingCode => _variableMappingCode;

    public string MethodCode => _methodCode;


    public void CreateVariableByDefaultMappingMethod(PropertyMappingModel mapping)
    {
        _variableCreationCode =
@$"{Constants.Indent__3}var {mapping.VariableName} = {mapping.DefaultMethodName}(source);
{Constants.Indent__3}{mapping.MethodName}(source, ref {mapping.VariableName});";
    }

    public void CreateVariableByRequiredMappingMethod(PropertyMappingModel mapping)
    {
        _variableCreationCode =
@$"{Constants.Indent__3}var {mapping.VariableName} = {mapping.MethodName}(source);";
    }

    public void SetVariableInConstructor(PropertyMappingModel mapping)
    {
        _constructorVariableMappingCode =
$@"{Constants.Indent____5}{mapping.PropertyName} : {mapping.VariableName}";
    }

    public void SetVariablePostConstructor(PropertyMappingModel mapping)
    {
        _postConstructorVariableMappingCode =
$@"{Constants.Indent____5}{mapping.PropertyName} = {mapping.VariableName}";
    }

    public void SetVariable(PropertyMappingModel mapping)
    {
        _variableMappingCode =
$@"{Constants.Indent___4}target.{mapping.PropertyName} = {mapping.VariableName};";
    }

    public void CreateDefaultMappingMethod(PropertyMappingModel mapping, AttributeDataModel attr, string code)
    {
        _methodCode = 
$@"{Constants.Indent_2}private static {mapping.PropertyType.ToDisplayString()} {mapping.DefaultMethodName}({attr.Source.ToDisplayString()} source)
{Constants.Indent_2}{{
{Constants.Indent__3}return {code};
{Constants.Indent_2}}}";
    }

    public void CreateOptionalMappingMethodToBeImplemented(PropertyMappingModel mapping, AttributeDataModel attr)
    {
       var code =
$@"{Constants.Indent_2}static partial void {mapping.MethodName}({attr.Source.ToDisplayString()} source, ref {mapping.PropertyType.ToDisplayString()} target);";
        _methodCode = $"{_methodCode}{Constants.NewLine}{code}";
    }

    public void CreateRequiredMappingMethodToBeImplemented(PropertyMappingModel mapping, AttributeDataModel attr)
    {
        _methodCode = 
$@"{Constants.Indent_2}private static partial {mapping.PropertyType.ToDisplayString()} {mapping.MethodName}({attr.Source.ToDisplayString()} source);";
    }
}
