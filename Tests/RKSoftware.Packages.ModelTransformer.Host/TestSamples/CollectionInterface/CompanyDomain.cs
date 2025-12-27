namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterface;

public class CompanyDomain
{
    public IList<ProjectDomain>? ProjectIList { get; set; }

    public IEnumerable<ProjectDomain>? ProjectIEnumerable { get; set; }

    public ICollection<ProjectDomain>? ProjectICollection { get; set; }

    public IReadOnlyCollection<ProjectDomain>? ProjectIReadOnlyCollection { get; set; }

    public IReadOnlyList<ProjectDomain>? ProjectIReadOnlyList { get; set; }
}
