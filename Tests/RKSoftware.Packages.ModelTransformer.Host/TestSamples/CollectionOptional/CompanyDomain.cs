namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionOptional;

public class CompanyDomain
{
    public List<ProjectDomain>? ProjectList { get; set; }

    public IList<ProjectDomain>? ProjectIList { get; set; }

    public IEnumerable<ProjectDomain>? ProjectIEnumerable { get; set; }

    public ICollection<ProjectDomain>? ProjectICollection { get; set; }

    public IReadOnlyCollection<ProjectDomain>? ProjectIReadOnlyCollection { get; set; }

    public IReadOnlyList<ProjectDomain>? ProjectIReadOnlyList { get; set; }

    public LinkedList<ProjectDomain>? ProjectLinkedList { get; set; }
}
