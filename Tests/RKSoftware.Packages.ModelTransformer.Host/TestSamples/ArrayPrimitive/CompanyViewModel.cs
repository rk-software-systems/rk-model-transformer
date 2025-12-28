namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitive;

public class CompanyViewModel
{
#pragma warning disable CA1819 // Properties should not return arrays
    public string[]? ProjectNames { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

#pragma warning disable CA1819 // Properties should not return arrays
    public int[]? ProjectIds { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
}
