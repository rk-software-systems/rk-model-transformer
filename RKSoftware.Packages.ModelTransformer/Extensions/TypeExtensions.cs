using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class TypeExtensions
{
    public static ITypeSymbol GetNonNullable(this ITypeSymbol type)
    {
        if (type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T && type is INamedTypeSymbol named)
        {
            return named.TypeArguments[0];
        }
        return type;
    }

    public static bool IsCollectionInterfaceSpecial(this INamedTypeSymbol type)
    {
        var specialType = type.OriginalDefinition.SpecialType;
        return specialType == SpecialType.System_Collections_Generic_IEnumerable_T ||
               specialType == SpecialType.System_Collections_Generic_ICollection_T ||
               specialType == SpecialType.System_Collections_Generic_IList_T ||
               specialType == SpecialType.System_Collections_Generic_IReadOnlyCollection_T ||
               specialType == SpecialType.System_Collections_Generic_IReadOnlyList_T;
    }

    public static bool IsGenericInterfaceConstructable(this ITypeSymbol type)
    {
        if (type is INamedTypeSymbol namedType && namedType.IsGenericType)
        {
            if (namedType.TypeArguments.Length != 1)
            {
                return false;
            }

            if (namedType.OriginalDefinition.IsCollectionInterfaceSpecial())
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsNullable(this ITypeSymbol type)
    {
        return type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T ||
               (type.IsReferenceType && type.NullableAnnotation == NullableAnnotation.Annotated);
    }
}
