using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class PropertyExtensions
{
    public static bool CanNotConvertType(this IPropertySymbol source, IPropertySymbol target, Compilation compilation)
    {
        // disallow nullable -> non-nullable
        if (source.IsNullable() && !target.IsNullable())
        {
            return true;
        }

        var sourceTypeNonNullable = source.Type.GetNonNullable();
        var targetTypeNonNullable = target.Type.GetNonNullable();

        if ((sourceTypeNonNullable.IsReferenceType && sourceTypeNonNullable.SpecialType != SpecialType.System_String) ||
            (targetTypeNonNullable.IsReferenceType && targetTypeNonNullable.SpecialType != SpecialType.System_String && targetTypeNonNullable.SpecialType != SpecialType.System_Object))
        {
            return true;
        }

        var conversion = compilation.ClassifyConversion(sourceTypeNonNullable, targetTypeNonNullable);

        return !conversion.IsImplicit;
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
