namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveWithoutDefaultMapping;

public static partial class CompanyDomainExtensions
{
    public const string Description = "Overridden Description";
    public const int EstablishedYear = 1000;

    private static partial string? ToCompanyViewModelDescription(CompanyDomain source)
    {
        return Description;
    }

    private static partial int? ToCompanyViewModelEstablishedYear(CompanyDomain source)
    {
        return EstablishedYear;
    }
}
