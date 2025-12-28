namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveNullableToNotNullable;

public class CompanyDomain
{
    public IList<string?>? ProjectIList { get; set; }

    public IEnumerable<int?>? ProjectIEnumerable { get; set; }

    public ICollection<DateTime?>? ProjectICollection { get; set; }

    public IReadOnlyCollection<long?>? ProjectIReadOnlyCollection { get; set; }

    public IReadOnlyList<byte?>? ProjectIReadOnlyList { get; set; }
}
