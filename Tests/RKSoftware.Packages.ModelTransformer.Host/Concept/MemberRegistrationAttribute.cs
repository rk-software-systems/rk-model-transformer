namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class MemberRegistrationAttribute<T1, T2>() : Attribute where T1 : class where T2 : class
{
    public string[]? IgnoredProperties { get; set; }
}
