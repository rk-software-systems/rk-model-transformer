namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfaceMissed;

public static partial class CompanyDomainExtensions
{
    private static partial IEnumerable<ProjectViewModel>? ToCompanyViewModelProjectIEnumerable(CompanyDomain source)
    {
        return source.ProjectIEnumerable?.Select(x => new ProjectViewModel { Id = x.Id, Name = x.Name });
    }

    private static partial IList<ProjectViewModel>? ToCompanyViewModelProjectIList(CompanyDomain source)
    {
        return source.ProjectIList?.Select(x => new ProjectViewModel { Id = x.Id, Name = x.Name }).ToList();
    }

    private static partial ICollection<ProjectViewModel>? ToCompanyViewModelProjectICollection(CompanyDomain source)
    {
        return source.ProjectICollection?.Select(x => new ProjectViewModel { Id = x.Id, Name = x.Name }).ToList();
    }

    private static partial IReadOnlyCollection<ProjectViewModel>? ToCompanyViewModelProjectIReadOnlyCollection(CompanyDomain source)
    {
        return source.ProjectIReadOnlyCollection?.Select(x => new ProjectViewModel { Id = x.Id, Name = x.Name }).ToList();
    }

    private static partial IReadOnlyList<ProjectViewModel>? ToCompanyViewModelProjectIReadOnlyList(CompanyDomain source)
    {
        return source.ProjectIReadOnlyList?.Select(x => new ProjectViewModel { Id = x.Id, Name = x.Name }).ToList();
    }
}
