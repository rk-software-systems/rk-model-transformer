namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayStructureNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial ProjectModel[]? ToCompanyViewModelProjects(CompanyDomain source)
    {
        return source.Projects?.Where(x => x.HasValue).Select(x => x!.Value).ToArray();
    }
}
