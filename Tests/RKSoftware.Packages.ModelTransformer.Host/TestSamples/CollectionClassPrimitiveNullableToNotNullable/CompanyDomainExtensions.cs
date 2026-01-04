using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassPrimitiveNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial List<string>? ToCompanyViewModelProjectList (CompanyDomain source)
    {
        return source.ProjectList?.Where(x => x != null).Select(x => x!).ToList();
    }

    private static partial Collection<int>?  ToCompanyViewModelProjectCollection (CompanyDomain source)
    {
        return source.ProjectCollection == null
            ? null
            : new Collection<int>([.. source.ProjectCollection.Where(x => x.HasValue).Select(x => x!.Value)]);
    }

    private static partial ReadOnlyCollection<long>?  ToCompanyViewModelProjectReadOnlyCollection (CompanyDomain source)
    {
        return source.ProjectReadOnlyCollection == null
            ? null
            : new ReadOnlyCollection<long>([.. source.ProjectReadOnlyCollection.Where(x => x.HasValue).Select(x => x!.Value)]);
    }

    private static partial LinkedList<DateTime>?  ToCompanyViewModelProjectLinkedList (CompanyDomain source)
    {
        return source.ProjectLinkedList == null
            ? null
            : new LinkedList<DateTime>([.. source.ProjectLinkedList.Where(x => x.HasValue).Select(x => x!.Value)]);
    }

    private static partial Queue<byte>?  ToCompanyViewModelProjectQueue (CompanyDomain source)
    {
        return source.ProjectQueue == null
            ? null
            : new Queue<byte>([.. source.ProjectQueue.Where(x => x.HasValue).Select(x => x!.Value)]);
    }

    private static partial Stack<string>?  ToCompanyViewModelProjectStack (CompanyDomain source)
    {
        return source.ProjectStack == null
            ? null
            : new Stack<string>([.. source.ProjectStack.Where(x => x != null).Select(x => x!)]);
    }

    private static partial HashSet<string>?  ToCompanyViewModelProjectHashSet (CompanyDomain source)
    {
        return source.ProjectHashSet == null
            ? null
            : new HashSet<string>([.. source.ProjectHashSet.Where(x => x != null).Select(x => x!)]);
    }
}
