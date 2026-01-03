using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassStructureNotNullableToNullable;

public class CompanyViewModel
{
    public List<ProjectModel?>? ProjectList { get; set; }

    public Collection<ProjectModel?>? ProjectCollection { get; set; }

    public ReadOnlyCollection<ProjectModel?>? ProjectReadOnlyCollection { get; set; }

    public LinkedList<ProjectModel?>? ProjectLinkedList { get; set; }

    public Queue<ProjectModel?>? ProjectQueue { get; set; }

    public Stack<ProjectModel?>? ProjectStack { get; set; }

    public HashSet<ProjectModel?>? ProjectHashSet { get; set; }
}
