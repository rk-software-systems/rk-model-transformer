using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Helpers;

internal static class PropertySymbolHelper
{
    public static bool CanNotConvertType(IPropertySymbol source, IPropertySymbol target)
    {
        var sourceTypeNonNullable = GetNonNullable(source.Type);
        var targetTypeNonNullable = GetNonNullable(target.Type);

        if (!sourceTypeNonNullable.Equals(targetTypeNonNullable, SymbolEqualityComparer.Default))
        {
            return true;
        }

        var isSourceNullable = IsNullable(source);
        var isTargetNullable = IsNullable(target);

        return !isTargetNullable && isSourceNullable;
    }

    private static bool IsNullable(IPropertySymbol prop)
    {
        return prop.Type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T ||
               (prop.Type.IsReferenceType && prop.NullableAnnotation == NullableAnnotation.Annotated);
    }

    private static ITypeSymbol GetNonNullable(ITypeSymbol type)
    {
        if (type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T && type is INamedTypeSymbol named)
        {
            return named.TypeArguments[0];
        }
        return type;
    }
}
