namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveNotNullableToNullable;

public class CompanyViewModel
{
    public IList<string?>? ProjectIList { get; set; }

    public IEnumerable<int?>? ProjectIEnumerable { get; set; }

    public ICollection<DateTime?>? ProjectICollection { get; set; }

    public IReadOnlyCollection<long?>? ProjectIReadOnlyCollection { get; set; }

    public IReadOnlyList<byte?>? ProjectIReadOnlyList { get; set; }
}
