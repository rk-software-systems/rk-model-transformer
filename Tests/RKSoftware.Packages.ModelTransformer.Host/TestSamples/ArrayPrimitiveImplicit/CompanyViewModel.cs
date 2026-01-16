namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitiveImplicit;

#pragma warning disable CA1819 // Properties should not return arrays

public class CompanyViewModel
{
    public object?[]? ProjectNames { get; set; }

    public long?[]? ProjectIds { get; set; }

    public decimal?[]? Prices { get; set; }
}
