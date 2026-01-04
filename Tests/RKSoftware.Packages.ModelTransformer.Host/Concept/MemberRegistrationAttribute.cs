namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class MemberRegistrationAttribute<T1, T2>() : Attribute where T1 : class where T2 : class
{
    /// <summary>
    /// The collection of property names to be ignored during the transformation. 
    /// </summary>
    public string[]? Ignored { get; init; }

    /// <summary>
    /// The collection of property names to be excluded from default mapping during the transformation.
    /// </summary>
    public string[]? WithoutDefaultMapping { get; init; }
}
