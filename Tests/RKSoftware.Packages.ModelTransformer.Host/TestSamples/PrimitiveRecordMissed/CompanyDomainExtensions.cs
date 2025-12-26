namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRecordMissed;

public static partial class CompanyDomainExtensions
{
    private static partial string ToCompanyViewModelName(CompanyDomain source)
    {
        return "Super Company";
    }
}
