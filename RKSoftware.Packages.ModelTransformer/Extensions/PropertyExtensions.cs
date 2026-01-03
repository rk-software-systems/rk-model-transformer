using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class PropertyExtensions
{
    public static bool CanNotConvertType(this IPropertySymbol source, IPropertySymbol target)
    {
        var sourceTypeNonNullable = source.Type.GetNonNullable();
        var targetTypeNonNullable = target.Type.GetNonNullable();

        if ((sourceTypeNonNullable.IsReferenceType && sourceTypeNonNullable.SpecialType != SpecialType.System_String) ||
            (targetTypeNonNullable.IsReferenceType && sourceTypeNonNullable.SpecialType != SpecialType.System_String) ||
            !sourceTypeNonNullable.Equals(targetTypeNonNullable, SymbolEqualityComparer.Default))
        {
            return true;
        }

        var isSourceNullable = source.IsNullable();
        var isTargetNullable = target.IsNullable();

        // disallow conversion from nullable to non-nullable
        return !isTargetNullable && isSourceNullable;
    }

    public static bool IsNullable(this IPropertySymbol prop)
    {
        // check for nullable value type or nullable reference type
        return prop.Type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T ||
               (prop.Type.IsReferenceType && prop.NullableAnnotation == NullableAnnotation.Annotated);
    }

    public static ITypeSymbol? GetGenericArgumentType(this IPropertySymbol property)
    {
        var type = property.Type;

        // handle primitive types and string
        if (type.IsPrimitiveOrString())
        {
            return null;
        }

        // handle array types
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

            if (namedType.OriginalDefinition.IsEnumerableInterface() || 
                namedType.OriginalDefinition.IsCollectionInterface() || 
                namedType.OriginalDefinition.IsListInterface() ||
                namedType.AllInterfaces.Any(i => i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
            {
                return namedType.TypeArguments[0];
            }
        }

        return null;
    } 
}
