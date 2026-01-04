namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexStructureNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    public static IndustryModel DefaultIndustry => new()
    {
        Id = 0,
        Name = "Default Industry"
    };

    private static partial IndustryModel ToCompanyViewModelIndustry(CompanyDomain source)
    {
        return source.Industry ?? DefaultIndustry;
    }

}
