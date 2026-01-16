namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitiveImplicit;

public static partial class CompanyDomainExtensions
{
    private static partial decimal?[]? ToCompanyViewModelPrices(CompanyDomain source)
    {
        return source.Prices != null ? [.. source.Prices.Select(x => (decimal?)x)] : default;
    }
}
