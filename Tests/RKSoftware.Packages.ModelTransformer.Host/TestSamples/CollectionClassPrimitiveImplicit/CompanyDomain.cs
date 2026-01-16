using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassPrimitiveImplicit;

public class CompanyDomain
{
    public List<string?>? ProjectList { get; set; }

    public Collection<int?>? ProjectCollection { get; set; }

    public ReadOnlyCollection<double?>? ProjectReadOnlyCollection { get; set; }
}
