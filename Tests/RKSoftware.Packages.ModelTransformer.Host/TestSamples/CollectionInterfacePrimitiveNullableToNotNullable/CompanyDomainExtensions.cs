namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial IEnumerable<int>? ToCompanyViewModelProjectIEnumerable(CompanyDomain source)
    {
        return source.ProjectIEnumerable?.Where(x => x.HasValue).Select(x => x!.Value);
    }

    private static partial IList<string>? ToCompanyViewModelProjectIList(CompanyDomain source)
    {
        return source.ProjectIList?.Where(x => x != null).Select(x => x!).ToList();
    }

    private static partial ICollection<DateTime>? ToCompanyViewModelProjectICollection(CompanyDomain source)
    {
        return source.ProjectICollection?.Where(x => x.HasValue).Select(x => x!.Value).ToList();
    }

    private static partial IReadOnlyCollection<long>? ToCompanyViewModelProjectIReadOnlyCollection(CompanyDomain source)
    {
        return source.ProjectIReadOnlyCollection?.Where(x => x.HasValue).Select(x => x!.Value).ToList();
    }

    private static partial IReadOnlyList<byte>? ToCompanyViewModelProjectIReadOnlyList(CompanyDomain source)
    {
        return source.ProjectIReadOnlyList?.Where(x => x.HasValue).Select(x => x!.Value).ToList();
    }
}
