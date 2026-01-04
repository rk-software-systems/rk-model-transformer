using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassPrimitiveNullable;

public class CompanyViewModel
{
    public List<string?>? ProjectList { get; set; }

    public Collection<int?>? ProjectCollection { get; set; }

    public ReadOnlyCollection<long?>? ProjectReadOnlyCollection { get; set; }

    public LinkedList<DateTime?>? ProjectLinkedList { get; set; }

    public Queue<byte?>? ProjectQueue { get; set; }

    public Stack<string?>? ProjectStack { get; set; }

    public HashSet<string?>? ProjectHashSet { get; set; }
}
