namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexStructurePostDefault;

public static partial class CompanyDomainExtensions
{
    public static IndustryModel OverriddenIndustry => new()
    {
        Id = 999,
        Name = "Overridden Industry"
    };

    static partial void ToCompanyViewModelIndustry(CompanyDomain source, ref IndustryModel? target)
    {
        target = OverriddenIndustry;
    }
}
