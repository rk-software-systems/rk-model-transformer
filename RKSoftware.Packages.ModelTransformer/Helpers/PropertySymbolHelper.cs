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

    public static bool IsNullable(IPropertySymbol prop)
    {
        return prop.Type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T ||
               (prop.Type.IsReferenceType && prop.NullableAnnotation == NullableAnnotation.Annotated);
    }

    public static ITypeSymbol? GetGenericElementType(IPropertySymbol property)
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

            if (namedType.AllInterfaces.Any(i => i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
            {
                return namedType.TypeArguments[0];
            }
        }

        return null;
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
