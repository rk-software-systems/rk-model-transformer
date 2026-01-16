namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitiveImplicit;

#pragma warning disable CA1819 // Properties should not return arrays

public class CompanyDomain
{
    public string?[]? ProjectNames { get; set; }

    public int?[]? ProjectIds { get; set; }

    public double?[]? Prices { get; set; }
}
