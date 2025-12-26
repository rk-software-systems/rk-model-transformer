namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionOptional;

public class CompanyViewModel
{
    public LinkedList<ProjectViewModel>? ProjectLinkedList { get; set; }

    public List<ProjectViewModel>? ProjectList { get; set; }

    public IList<ProjectViewModel>? ProjectIList { get; set; }

    public IEnumerable<ProjectViewModel>? ProjectIEnumerable { get; set; }

    public ICollection<ProjectViewModel>? ProjectICollection { get; set; }

    public IReadOnlyCollection<ProjectViewModel>? ProjectIReadOnlyCollection { get; set; }

    public IReadOnlyList<ProjectViewModel>? ProjectIReadOnlyList { get; set; }    
}
