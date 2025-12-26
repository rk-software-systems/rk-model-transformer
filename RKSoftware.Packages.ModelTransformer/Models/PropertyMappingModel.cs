using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Extensions;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class PropertyMappingModel
{
    public string PropertyName { get; }

    public ITypeSymbol PropertyType { get; }

    public bool IsIgnored { get; set; }

    public bool IsNullable { get; }

    public string VariableName { get; } 

    public string DefaultMethodName { get; }

    public string MethodName { get; }

    public string VariableCreationCode { get; set; } = string.Empty;

    public string ConstructorVariableMappingCode { get; set; } = string.Empty;

    public string PostConstructorVariableMappingCode { get; set; } = string.Empty;

    public string VariableMappingCode { get; set; } = string.Empty;

    public string MethodCode { get; set; } = string.Empty;

    public bool IsReadonly { get; }

    public bool IsConstructorParam { get; }    

    #region ctor

    public PropertyMappingModel(IPropertySymbol targetProperty, AttributeDataModel attr, bool isIgnored)
    {
        PropertyName = targetProperty.Name;
        PropertyType = targetProperty.Type;
        IsIgnored = isIgnored;
        IsNullable = targetProperty.IsNullable();
        VariableName = isIgnored ? Constants.Default : targetProperty.Name.LowerCaseFirstLetter();
        DefaultMethodName = $"To{attr.Target.Name}{targetProperty.Name}Default";
        MethodName = $"To{attr.Target.Name}{targetProperty.Name}";
        IsReadonly = targetProperty.SetMethod == null || targetProperty.SetMethod.IsInitOnly;
        IsConstructorParam = attr.TargetConstructorParams.TryGetValue(targetProperty.Name, out _);        
    }

    #endregion
}
