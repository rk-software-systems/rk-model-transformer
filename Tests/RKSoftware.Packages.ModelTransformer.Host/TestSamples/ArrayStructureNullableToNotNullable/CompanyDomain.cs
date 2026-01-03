namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayStructureNullableToNotNullable;

public class CompanyDomain
{
#pragma warning disable CA1819 // Properties should not return arrays
    public ProjectModel?[]? Projects { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
}
