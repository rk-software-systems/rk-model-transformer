using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassNotNullableToNullable;

public class CompanyViewModel
{
    public List<ProjectViewModel?>? ProjectList { get; set; }

    public Collection<ProjectViewModel?>? ProjectCollection { get; set; }

    public ReadOnlyCollection<ProjectViewModel?>? ProjectReadOnlyCollection { get; set; }

    public LinkedList<ProjectViewModel?>? ProjectLinkedList { get; set; }

    public Queue<ProjectViewModel?>? ProjectQueue { get; set; }

    public Stack<ProjectViewModel?>? ProjectStack { get; set; }

    public HashSet<ProjectViewModel?>? ProjectHashSet { get; set; }
}
