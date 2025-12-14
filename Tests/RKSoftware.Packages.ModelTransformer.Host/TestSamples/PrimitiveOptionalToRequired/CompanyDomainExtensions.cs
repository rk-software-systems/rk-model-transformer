namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveOptionalToRequired;

public static partial class CompanyDomainExtensions
{
    public const string Title = "Remote Company Title";
    public const int BrandId = 7;

    private static partial string ToCompanyViewModelTitle(CompanyDomain source)
    {
        return Title;
    }

    private static partial int ToCompanyViewModelBrandId(CompanyDomain source)
    {
        return BrandId;
    }
}
