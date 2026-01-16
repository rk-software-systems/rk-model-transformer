using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveImplicit;

public static partial class CompanyDomainExtensions
{
    private static partial ICollection<decimal?>? ToCompanyViewModelProjectICollection(CompanyDomain source)
    {
        return source.ProjectICollection != null ?
            [.. source.ProjectICollection.Select(x => (decimal?)x)]
            : null;
    }
}
