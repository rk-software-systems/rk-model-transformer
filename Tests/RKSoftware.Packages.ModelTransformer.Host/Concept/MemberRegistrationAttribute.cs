namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class MemberRegistrationAttribute<T1, T2>() : Attribute
{
    public string[]? IgnoredProperties { get; set; }

    public string MethodName { get; set; } = "Transform";
}
