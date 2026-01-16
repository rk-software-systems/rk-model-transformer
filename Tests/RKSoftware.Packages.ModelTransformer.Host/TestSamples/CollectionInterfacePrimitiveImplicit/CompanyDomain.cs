namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveImplicit;

public class CompanyDomain
{
    public IList<string?>? ProjectIList { get; set; }

    public IEnumerable<int?>? ProjectIEnumerable { get; set; }

    public ICollection<double?>? ProjectICollection { get; set; }
}
