using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Helpers;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class PropertyMappingModel
{
    public string PropertyName { get; }

    public string VariableName { get; } 

    public string DefaultMethodName { get; }

    public string MethodName { get; }

    public string VariableCreationCode { get; set; } = string.Empty;

    public string VariableMappingCode { get; set; } = string.Empty;

    public string ConstructorVariableMappingCode { get; set; } = string.Empty;

    public string MethodCode { get; set; } = string.Empty;

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
