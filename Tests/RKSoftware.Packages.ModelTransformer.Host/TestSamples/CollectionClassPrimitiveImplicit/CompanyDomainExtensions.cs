using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassPrimitiveImplicit;

public static partial class CompanyDomainExtensions
{
    private static partial ReadOnlyCollection<decimal?>? ToCompanyViewModelProjectReadOnlyCollection(CompanyDomain source)
    {
        return source.ProjectReadOnlyCollection != null ?
            [.. source.ProjectReadOnlyCollection.Select(x => (decimal?)x)]
            : null;
    }
}
