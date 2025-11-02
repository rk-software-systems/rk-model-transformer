namespace RKSoftware.Packages.ModelTransformer.Host;

public static partial class DomainExtensions
{
    private static partial string? ToViewModelStringMissed(Domain source)
    {
        return "DefaultMissedValue";
    }

    private static partial string? ToDtoStringMissed(Domain source)
    {
        return "DefaultMissedValue";
    }
}
