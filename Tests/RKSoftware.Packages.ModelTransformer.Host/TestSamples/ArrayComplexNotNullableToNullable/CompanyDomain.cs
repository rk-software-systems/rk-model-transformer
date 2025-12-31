namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayComplexNotNullableToNullable;

public class CompanyDomain
{
#pragma warning disable CA1819 // Properties should not return arrays
    public ProjectDomain[]? Projects { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
}
