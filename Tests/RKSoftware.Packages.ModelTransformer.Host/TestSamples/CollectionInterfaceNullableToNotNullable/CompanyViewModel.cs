namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfaceNullableToNotNullable;

public class CompanyViewModel
{
    public IList<ProjectViewModel>? ProjectIList { get; set; }

    public IEnumerable<ProjectViewModel>? ProjectIEnumerable { get; set; }

    public ICollection<ProjectViewModel>? ProjectICollection { get; set; }

    public IReadOnlyCollection<ProjectViewModel>? ProjectIReadOnlyCollection { get; set; }

    public IReadOnlyList<ProjectViewModel>? ProjectIReadOnlyList { get; set; }
}
