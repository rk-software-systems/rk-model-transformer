using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial List<ProjectViewModel>? ToCompanyViewModelProjectList(CompanyDomain source)
    {
        return source.ProjectList?.Where(x => x!= null).Select(p => p!.Transform()).ToList();
    }

    private static partial Collection<ProjectViewModel>? ToCompanyViewModelProjectCollection(CompanyDomain source)
    {
        return source.ProjectCollection != null ?
            [.. source.ProjectCollection.Where(x => x != null).Select(p => p!.Transform())]:
            null;
    }

    private static partial ReadOnlyCollection<ProjectViewModel>? ToCompanyViewModelProjectReadOnlyCollection(CompanyDomain source)
    {
        return source.ProjectReadOnlyCollection != null ?
            [.. source.ProjectReadOnlyCollection.Where(x => x != null).Select(p => p!.Transform())] :
            null;
    }

    private static partial LinkedList<ProjectViewModel>? ToCompanyViewModelProjectLinkedList(CompanyDomain source)
    {
        return source.ProjectLinkedList != null ?
            new LinkedList<ProjectViewModel>(source.ProjectLinkedList.Where(x => x != null).Select(p => p!.Transform())) :
            null;
    }

    private static partial Queue<ProjectViewModel>? ToCompanyViewModelProjectQueue(CompanyDomain source)
    {
        return source.ProjectQueue != null ?
            new Queue<ProjectViewModel>(source.ProjectQueue.Where(x => x != null).Select(p => p!.Transform())) :
            null;
    }

    private static partial Stack<ProjectViewModel>? ToCompanyViewModelProjectStack(CompanyDomain source)
    {
        return source.ProjectStack != null ?
            new Stack<ProjectViewModel>(source.ProjectStack.Where(x => x != null).Select(p => p!.Transform())) :
            null;
    }

    private static partial HashSet<ProjectViewModel>? ToCompanyViewModelProjectHashSet(CompanyDomain source)
    {
        return source.ProjectHashSet != null ?
            [.. source.ProjectHashSet.Where(x => x != null).Select(p => p!.Transform())] :
            null;
    }
}
