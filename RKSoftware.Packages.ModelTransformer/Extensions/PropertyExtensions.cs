using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class PropertyExtensions
{
    public static bool CanNotConvertType(this IPropertySymbol source, IPropertySymbol target)
    {
        var sourceTypeNonNullable = source.Type.GetNonNullable();
        var targetTypeNonNullable = target.Type.GetNonNullable();

        if (!sourceTypeNonNullable.Equals(targetTypeNonNullable, SymbolEqualityComparer.Default))
        {
            return true;
        }

        var isSourceNullable = IsNullable(source);
        var isTargetNullable = IsNullable(target);

        return !isTargetNullable && isSourceNullable;
    }

    public static bool IsNullable(this IPropertySymbol prop)
    {
        return prop.Type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T ||
               (prop.Type.IsReferenceType && prop.NullableAnnotation == NullableAnnotation.Annotated);
    }

    public static ITypeSymbol? GetGenericArgumentType(this IPropertySymbol property)
    {
        var type = property.Type;

        if (type.SpecialType == SpecialType.System_String)
        {
            return null;
        }

        if (type.IsValueType && type.SpecialType != SpecialType.None)
        {
            return null;
        }

        if (type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T &&
            type is INamedTypeSymbol nt &&
            nt.TypeArguments.Length == 1 &&
            nt.TypeArguments[0].SpecialType != SpecialType.None)
        {
            return null;
        }

        if (type is IArrayTypeSymbol arrayType)
        {
            return arrayType.ElementType;
        }

        if (type is INamedTypeSymbol namedType && namedType.IsGenericType)
        {
            if (namedType.TypeArguments.Length != 1)
            {
                return null;
            }

            if (namedType.OriginalDefinition.SpecialType.IsCollectionInterfaceSpecial() ||
                namedType.AllInterfaces.Any(i => i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
            {
                return namedType.TypeArguments[0];
            }
        }

        return null;
    }

    public static bool IsGenericInterfaceConstructable(this ITypeSymbol type)
    {
        if (type is INamedTypeSymbol namedType && namedType.IsGenericType)
        {
            if (namedType.TypeArguments.Length != 1)
            {
                return false;
            }

            var specialType = namedType.OriginalDefinition.SpecialType;
            if (specialType.IsCollectionInterfaceSpecial())
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsCollectionInterfaceSpecial(this SpecialType specialType)
    {
        return specialType == SpecialType.System_Collections_Generic_IEnumerable_T ||
               specialType == SpecialType.System_Collections_Generic_ICollection_T ||
               specialType == SpecialType.System_Collections_Generic_IList_T ||
               specialType == SpecialType.System_Collections_Generic_IReadOnlyCollection_T ||
               specialType == SpecialType.System_Collections_Generic_IReadOnlyList_T;
    }

    private static ITypeSymbol GetNonNullable(this ITypeSymbol type)
    {
        if (type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T && type is INamedTypeSymbol named)
        {
            return named.TypeArguments[0];
        }
        return type;
    }
}
