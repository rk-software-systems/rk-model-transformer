namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexMissedInRegistration;

public static partial class CompanyDomainExtensions
{
    private static partial IndustryViewModel? ToCompanyViewModelIndustry(CompanyDomain source)
    {
        return source.Industry != null ? new IndustryViewModel
        {
            Id = source.Industry.Id,
            Name = source.Industry.Name,
        }: null;
    }
}
