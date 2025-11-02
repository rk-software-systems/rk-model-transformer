using System.Text;
using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Helpers;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal class PropertyMappingModel(IPropertySymbol target)
{
    public string PropertyName { get; } = target.Name;

    public string VariableName { get; } = StringHelper.LowerCaseFirstLetter(target.Name);

    public string DefaultMethodName { get; } = $"To{target.Name}Default";

    public string MethodName { get; } = $"To{target.Name}";

    public StringBuilder VariableCreationCode { get; set; } = new StringBuilder();

    public StringBuilder VariableMappingCode { get; set; } = new StringBuilder();

    public StringBuilder MethodCode { get; set; } = new StringBuilder();
}
