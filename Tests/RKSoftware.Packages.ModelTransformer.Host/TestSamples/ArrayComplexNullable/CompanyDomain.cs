namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayComplexNullable;

public class CompanyDomain
{
#pragma warning disable CA1819 // Properties should not return arrays
    public ProjectDomain?[]? Projects { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
}
