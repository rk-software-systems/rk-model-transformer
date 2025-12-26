namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveMissed;

public static partial class CompanyDomainExtensions
{
    private static partial string? ToCompanyViewModelPrincipalName(CompanyDomain source)
    {
        return "Mr. Anderson";
    }

    private static partial int? ToCompanyViewModelProjectCount(CompanyDomain source)
    {
        return 42;
    }
}
