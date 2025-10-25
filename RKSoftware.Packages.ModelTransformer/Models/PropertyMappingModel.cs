using System.Text;

namespace RKSoftware.Packages.ModelTransformer;

internal class PropertyMappingModel
{
    public string PropertyName { get; set; } = string.Empty;

    public string VariableName { get; set; } = string.Empty;

    public StringBuilder VariableCreationCode { get; set; } = new StringBuilder();

    public StringBuilder VariableMappingCode { get; set; } = new StringBuilder();

    public StringBuilder MethodCode { get; set; } = new StringBuilder();    
}
