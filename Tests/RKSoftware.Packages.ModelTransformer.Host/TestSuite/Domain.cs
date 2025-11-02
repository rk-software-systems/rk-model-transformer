namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

public class Domain
{
    public string? StringOptional { get; set; }

    public required string StringRequired { get; set; }

    public string? StringIgnored { get; set; }
}
