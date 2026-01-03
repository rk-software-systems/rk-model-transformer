using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class TypeExtensions
{
    /// <summary>
    /// Returns the underlying non-nullable type for a nullable value type, or the original type if it is not nullable.
    /// </summary>
    /// <remarks>
    /// This method is typically used when analyzing type symbols to determine the actual value type
    /// underlying a nullable type (e.g., extracting 'int' from 'int?'). If the provided type is not a nullable value
    /// type, the original type is returned unchanged.
    /// </remarks>
    /// <param name="type">
    /// The type symbol to evaluate. This can represent any type, including nullable value types.
    /// </param>
    /// <returns>
    /// An <see cref="ITypeSymbol"/> representing the non-nullable underlying type if <paramref name="type"/> is a
    /// nullable value type; otherwise, <paramref name="type"/> itself.
    /// </returns>
    public static ITypeSymbol GetNonNullable(this ITypeSymbol type)
    {
        if (type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T && type is INamedTypeSymbol named)
        {
            return named.TypeArguments[0];
        }
        return type;
    }

    public static bool IsEnumerableInterface(this INamedTypeSymbol type)
    {
        var specialType = type.OriginalDefinition.SpecialType;
        return specialType == SpecialType.System_Collections_Generic_IEnumerable_T;
    }

    /// <summary>
    /// Determines whether the specified type symbol represents a generic IList{T} or IReadOnlyList{T} interface.
    /// </summary>
    /// <param name="type">
    /// The type symbol to evaluate. Must be a named type symbol representing an interface.</param>
    /// <returns>
    /// true if the type is the generic IList{T} or IReadOnlyList{T} interface; otherwise, false.
    /// </returns>
    public static bool IsListInterface(this INamedTypeSymbol type)
    {
        var specialType = type.OriginalDefinition.SpecialType;
        return specialType == SpecialType.System_Collections_Generic_IList_T ||
               specialType == SpecialType.System_Collections_Generic_IReadOnlyList_T;
    }

    /// <summary>
    /// Determines whether the specified type represents a generic collection interface that is considered special, such
    /// as ICollection{T} or IReadOnlyCollection{T}.
    /// </summary>
    /// <param name="type">
    /// The type symbol to evaluate. Must represent a named type.
    /// </param>
    /// <returns>
    /// true if the type is ICollection{T} or IReadOnlyCollection{T}; otherwise, false.
    /// </returns>
    public static bool IsCollectionInterface(this INamedTypeSymbol type)
    {
        var specialType = type.OriginalDefinition.SpecialType;
        return specialType == SpecialType.System_Collections_Generic_ICollection_T ||
               specialType == SpecialType.System_Collections_Generic_IReadOnlyCollection_T;
    }

    /// <summary>
    /// Determines whether the specified type symbol represents a generic collection interface that can be constructed
    /// with a single type argument.
    /// </summary>
    /// <remarks>
    /// This method is typically used to identify interfaces such as IEnumerable{T}, ICollection{T},
    /// or IList{T} that can be constructed with a single generic type parameter. The method returns false for
    /// non-generic types, types with more than one type argument, or types that are not recognized as collection
    /// interfaces.
    /// </remarks>
    /// <param name="type">
    /// The type symbol to evaluate. Must represent a named generic type.
    /// </param>
    /// <returns>
    /// true if the type is a generic collection interface with exactly one type argument; otherwise, false.
    /// </returns>
    public static bool IsGenericInterfaceConstructable(this ITypeSymbol type)
    {
        if (type is INamedTypeSymbol namedType && namedType.IsGenericType)
        {
            if (namedType.TypeArguments.Length != 1)
            {
                return false;
            }

            var originalType = namedType.OriginalDefinition;
            if (originalType.IsEnumerableInterface() ||
                originalType.IsCollectionInterface() ||
                originalType.IsListInterface())
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Determines whether the specified type symbol represents an array type.
    /// </summary>
    /// <param name="type">The type symbol to evaluate. Cannot be null.</param>
    /// <returns>true if the type symbol represents an array type; otherwise, false.</returns>
    public static bool IsArrayType(this ITypeSymbol type)
    {
        return type is IArrayTypeSymbol;
    }

    /// <summary>
    /// Determines whether the specified type symbol represents a nullable type.
    /// </summary>
    /// <remarks>
    /// This method considers both value types declared as Nullable{T} and reference types annotated
    /// as nullable (e.g., string?). Use this method to check if a type can represent a null value in the context of
    /// nullable reference types and value types.
    /// </remarks>
    /// <param name="type">
    /// The type symbol to evaluate for nullability. This can be a value type or a reference type.
    /// </param>
    /// <returns>
    /// true if the type is a nullable value type or an annotated reference type; otherwise, false.
    /// </returns>
    public static bool IsNullable(this ITypeSymbol type)
    {
        return type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T ||
               (type.IsReferenceType && type.NullableAnnotation == NullableAnnotation.Annotated);
    }

    /// <summary>
    /// Determines whether the specified type symbol represents a primitive type, a string, or a nullable primitive
    /// type.
    /// </summary>
    /// <remarks>
    /// This method considers types such as <see cref="System.Int32"/>, <see cref="System.Boolean"/>,
    /// <see cref="System.String"/>, and <see cref="System.Nullable{T}"/> where T is a primitive type as primitive or
    /// string types. Complex types, enums, and user-defined structs are not considered primitive by this
    /// method.
    /// </remarks>
    /// <param name="type">
    /// The type symbol to evaluate. This must be an instance of <see cref="ITypeSymbol"/>.</param>
    /// <returns>
    /// true if the type is a primitive type, a string, or a nullable primitive type; otherwise false.
    /// </returns>
    public static bool IsPrimitiveOrString(this ITypeSymbol type)
    {
        // handle string
        if (type.SpecialType == SpecialType.System_String)
        {
            return true;
        }

        // handle primitive types
        if (type.IsValueType && type.SpecialType != SpecialType.None)
        {
            return true;
        }

        // handle nullable<T> where T is a primitive type
        if (type.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T &&
            type is INamedTypeSymbol nt &&
            nt.TypeArguments.Length == 1 &&
            nt.TypeArguments[0].SpecialType != SpecialType.None)
        {
            return true;
        }

        return false;
    }

    public static bool IsStructure(this ITypeSymbol type)
    {
        return type.TypeKind == TypeKind.Struct;
    }

    public static ITypeSymbol? GetEnumerableParameterInConstructor(this ITypeSymbol type)
    {
        if (type is INamedTypeSymbol namedType && namedType.IsGenericType)
        {
            if (namedType.AllInterfaces.Any(i => i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
            {
                var constructor = namedType.Constructors
                    .FirstOrDefault(m => m.MethodKind == MethodKind.Constructor &&
                                         m.Parameters.Length == 1 &&
                                         m.Parameters[0].Type is INamedTypeSymbol paramType &&
                                         paramType.IsGenericType &&
                                         (paramType.OriginalDefinition.IsEnumerableInterface() || 
                                          paramType.OriginalDefinition.IsCollectionInterface() ||
                                          paramType.OriginalDefinition.IsListInterface() ||
                                          paramType.IsArrayType() || 
                                          namedType.AllInterfaces.Any(i => i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T)));
                return constructor?.Parameters[0].Type;
            }
        }
        return null;
    }    

    public static bool IsDictionaryType(this ITypeSymbol property)
    {
        return false;
    }
}
