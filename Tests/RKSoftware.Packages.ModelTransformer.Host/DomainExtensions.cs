namespace RKSoftware.Packages.ModelTransformer.Host;

public static partial class DomainExtensions
{
    private static partial string? ToStringMissed(Domain source)
    {
        return "DefaultMissedValue";
    }
}
