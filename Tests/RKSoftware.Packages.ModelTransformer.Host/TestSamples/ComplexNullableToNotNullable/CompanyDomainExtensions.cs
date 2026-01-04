namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    public static IndustryViewModel DefaultIndustry => new() { 
        Id = 0, 
        Name = "Unknown"     
    };

    private static partial IndustryViewModel ToCompanyViewModelIndustry(CompanyDomain source)
    {
        return source.Industry?.Transform() ?? DefaultIndustry;
    }

}
