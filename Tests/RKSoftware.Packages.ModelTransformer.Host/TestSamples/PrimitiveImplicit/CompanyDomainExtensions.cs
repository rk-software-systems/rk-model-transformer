namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveImplicit;

public static partial class CompanyDomainExtensions
{
    private static partial short? ToCompanyViewModelNumberOfAccountants(CompanyDomain source)
    {
        return (short?)source.NumberOfAccountants;
    }

    private static partial double? ToCompanyViewModelAverageOfSalary(CompanyDomain source)
    {
        return (double?)source.AverageOfSalary;
    }
}
