namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayComplexNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial ProjectViewModel[]? ToCompanyViewModelProjects(CompanyDomain source)
    {
        return source.Projects?.Where(x => x != null).Select(x => x!.Transform()).ToArray();
    }
}
