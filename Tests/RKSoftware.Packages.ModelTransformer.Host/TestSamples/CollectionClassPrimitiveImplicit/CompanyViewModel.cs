using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassPrimitiveImplicit;

public class CompanyViewModel
{
    public List<object?>? ProjectList { get; set; }

    public Collection<long?>? ProjectCollection { get; set; }

    public ReadOnlyCollection<decimal?>? ProjectReadOnlyCollection { get; set; }
}
