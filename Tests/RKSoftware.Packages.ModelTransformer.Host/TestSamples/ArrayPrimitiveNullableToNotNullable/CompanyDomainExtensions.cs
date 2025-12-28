namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitiveNullableToNotNullable;

public static partial class CompanyDomainExtensions
{
    private static partial string[]? ToCompanyViewModelProjectNames(CompanyDomain source)
    {
        return source.ProjectNames?.Where(x => x != null).Select(x => x!).ToArray();
    }

    private static partial int[]? ToCompanyViewModelProjectIds(CompanyDomain source)
    {
        return source.ProjectIds?.Where(x => x != null).Select(x => x!.Value).ToArray();
    }
}
