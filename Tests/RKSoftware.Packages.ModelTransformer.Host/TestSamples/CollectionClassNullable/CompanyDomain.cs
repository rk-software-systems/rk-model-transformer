using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassNullable;

public class CompanyDomain
{
    public List<ProjectDomain?>? ProjectList { get; set; }

    public Collection<ProjectDomain?>? ProjectCollection { get; set; }

    public ReadOnlyCollection<ProjectDomain?>? ProjectReadOnlyCollection { get; set; }

    public LinkedList<ProjectDomain?>? ProjectLinkedList { get; set; }

    public Queue<ProjectDomain?>? ProjectQueue { get; set; }

    public Stack<ProjectDomain?>? ProjectStack { get; set; }

    public HashSet<ProjectDomain?>? ProjectHashSet { get; set; }
}
