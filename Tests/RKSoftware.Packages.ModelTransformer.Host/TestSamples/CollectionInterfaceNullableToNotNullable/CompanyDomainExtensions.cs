namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfaceNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial IEnumerable<ProjectViewModel>? ToCompanyViewModelProjectIEnumerable(CompanyDomain source)
    {
        return source.ProjectIEnumerable?.Where(x => x != null).Select(x => x!.Transform());
    }

    private static partial IList<ProjectViewModel>? ToCompanyViewModelProjectIList(CompanyDomain source)
    {
        return source.ProjectIList?.Where(x => x != null).Select(x => x!.Transform()).ToList();
    }

    private static partial ICollection<ProjectViewModel>? ToCompanyViewModelProjectICollection(CompanyDomain source)
    {
        return source.ProjectICollection?.Where(x => x != null).Select(x => x!.Transform()).ToList();
    }

    private static partial IReadOnlyCollection<ProjectViewModel>? ToCompanyViewModelProjectIReadOnlyCollection(CompanyDomain source)
    {
        return source.ProjectIReadOnlyCollection?.Where(x => x != null).Select(x => x!.Transform()).ToList();
    }

    private static partial IReadOnlyList<ProjectViewModel>? ToCompanyViewModelProjectIReadOnlyList(CompanyDomain source)
    {
        return source.ProjectIReadOnlyList?.Where(x => x != null).Select(x => x!.Transform()).ToList();
    }
}
