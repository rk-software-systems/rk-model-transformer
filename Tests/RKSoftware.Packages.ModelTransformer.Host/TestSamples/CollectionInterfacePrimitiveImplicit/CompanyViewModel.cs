namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveImplicit;

public class CompanyViewModel
{
    public IList<object?>? ProjectIList { get; set; }

    public IEnumerable<long?>? ProjectIEnumerable { get; set; }

    public ICollection<decimal?>? ProjectICollection { get; set; }
}
