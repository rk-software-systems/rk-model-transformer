namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveImplicitToAndFromObject;

public static partial class CompanyDomainExtensions
{
    private static partial string? ToCompanyViewModelName(CompanyDomain source)
    {
        return (string?)source.Name;
    }

    private static partial int? ToCompanyViewModelEmployeeCount(CompanyDomain source)
    {
        return (int?)source.EmployeeCount;
    }
}
