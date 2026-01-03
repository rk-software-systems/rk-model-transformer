namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfaceStructureNullable;

public class CompanyViewModel
{
    public IList<ProjectModel?>? ProjectIList { get; set; }

    public IEnumerable<ProjectModel?>? ProjectIEnumerable { get; set; }

    public ICollection<ProjectModel?>? ProjectICollection { get; set; }

    public IReadOnlyCollection<ProjectModel?>? ProjectIReadOnlyCollection { get; set; }

    public IReadOnlyList<ProjectModel?>? ProjectIReadOnlyList { get; set; }
}
