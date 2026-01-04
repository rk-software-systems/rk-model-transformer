namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitivePostDefault;

public static partial class CompanyDomainExtensions
{
    public const string OverriddenDescription = "Custom Overridden Description";
    public const int OverriddenEstablishedYear = 1900;

    static partial void ToCompanyViewModelDescription(CompanyDomain source, ref string? target)
    {
        target = OverriddenDescription;
    }

    static partial void ToCompanyViewModelEstablishedYear(CompanyDomain source, ref int? target)
    {
        target = OverriddenEstablishedYear;
    }
}
