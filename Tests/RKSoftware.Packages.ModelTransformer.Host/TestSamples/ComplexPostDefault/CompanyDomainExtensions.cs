namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexPostDefault;

public static partial class CompanyDomainExtensions
{
    public static IndustryViewModel OverriddenIndustry => new()
    {
        Id = 999,
        Name = "Overridden Industry"
    };

    static partial void ToCompanyViewModelIndustry(CompanyDomain source, ref IndustryViewModel? target)
    {
        target = OverriddenIndustry;
    }
}
