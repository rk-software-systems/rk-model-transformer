namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayComplexNullableToNotNullable;

public class CompanyViewModel
{
#pragma warning disable CA1819 // Properties should not return arrays
    public ProjectViewModel[]? Projects { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
}
