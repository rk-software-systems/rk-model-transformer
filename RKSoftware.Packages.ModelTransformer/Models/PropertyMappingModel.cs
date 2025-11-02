using System.Text;
using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Helpers;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class PropertyMappingModel
{
    public string PropertyName { get; }

    public string VariableName { get; } 

    public string DefaultMethodName { get; }

    public string MethodName { get; }

    public StringBuilder VariableCreationCode { get; set; } = new StringBuilder();

    public StringBuilder VariableMappingCode { get; set; } = new StringBuilder();

    public StringBuilder MethodCode { get; set; } = new StringBuilder();

    #region ctor

    public PropertyMappingModel(IPropertySymbol targetProperty, ITypeSymbol target)
    {
        PropertyName = targetProperty.Name;
        VariableName = StringHelper.LowerCaseFirstLetter(targetProperty.Name);
        DefaultMethodName = $"To{target.Name}{targetProperty.Name}Default";
        MethodName = $"To{target.Name}{targetProperty.Name}";

    }

    #endregion
}
