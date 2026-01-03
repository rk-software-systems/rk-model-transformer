using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassStructureNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial List<ProjectModel>? ToCompanyViewModelProjectList(CompanyDomain source)
    {
        return source.ProjectList?
            .Where(item => item.HasValue)
            .Select(item => item!.Value)
            .ToList();
    }

    private static partial Collection<ProjectModel>? ToCompanyViewModelProjectCollection(CompanyDomain source)
    {
        return source.ProjectCollection != null ?
            new Collection<ProjectModel>([.. source.ProjectCollection.Where(x => x.HasValue).Select(x => x!.Value)]) :  
            null;
    }

    private static partial ReadOnlyCollection<ProjectModel>? ToCompanyViewModelProjectReadOnlyCollection(CompanyDomain source)
    {
        return source.ProjectReadOnlyCollection != null ?
            new ReadOnlyCollection<ProjectModel>([.. source.ProjectReadOnlyCollection.Where(x => x.HasValue).Select(x => x!.Value)]) :  
            null;
    }

    private static partial LinkedList<ProjectModel>? ToCompanyViewModelProjectLinkedList(CompanyDomain source)
    {
        return source.ProjectLinkedList != null ?
            new LinkedList<ProjectModel>([.. source.ProjectLinkedList.Where(x => x.HasValue).Select(x => x!.Value)]) :  
            null;
    }

    private static partial Queue<ProjectModel>? ToCompanyViewModelProjectQueue(CompanyDomain source)
    {
        return source.ProjectQueue != null ?
            new Queue<ProjectModel>([.. source.ProjectQueue.Where(x => x.HasValue).Select(x => x!.Value)]) :  
            null;
    }

    private static partial Stack<ProjectModel>? ToCompanyViewModelProjectStack(CompanyDomain source)
    {
        return source.ProjectStack != null ?
            new Stack<ProjectModel>([.. source.ProjectStack.Where(x => x.HasValue).Select(x => x!.Value)]) :  
            null;
    }

    private static partial HashSet<ProjectModel>? ToCompanyViewModelProjectHashSet(CompanyDomain source)
    {
        return source.ProjectHashSet != null ?
            new HashSet<ProjectModel>([.. source.ProjectHashSet.Where(x => x.HasValue).Select(x => x!.Value)]) :  
            null;
    }
}
