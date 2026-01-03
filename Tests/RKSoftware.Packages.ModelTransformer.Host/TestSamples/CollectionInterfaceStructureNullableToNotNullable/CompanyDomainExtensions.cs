namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfaceStructureNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial IEnumerable<ProjectModel>? ToCompanyViewModelProjectIEnumerable(CompanyDomain source)
    {
        return source.ProjectIEnumerable?.Where(x => x != null).Select(x => x!.Value);
    }

    private static partial IList<ProjectModel>? ToCompanyViewModelProjectIList(CompanyDomain source)
    {
        return source.ProjectIList?.Where(x => x != null).Select(x => x!.Value).ToList();
    }

    private static partial ICollection<ProjectModel>? ToCompanyViewModelProjectICollection(CompanyDomain source)
    {
        return source.ProjectICollection?.Where(x => x != null).Select(x => x!.Value).ToList();
    }

    private static partial IReadOnlyCollection<ProjectModel>? ToCompanyViewModelProjectIReadOnlyCollection(CompanyDomain source)
    {
        return source.ProjectIReadOnlyCollection?.Where(x => x != null).Select(x => x!.Value).ToList();
    }

    private static partial IReadOnlyList<ProjectModel>? ToCompanyViewModelProjectIReadOnlyList(CompanyDomain source)
    {
        return source.ProjectIReadOnlyList?.Where(x => x != null).Select(x => x!.Value).ToList();
    }
}
