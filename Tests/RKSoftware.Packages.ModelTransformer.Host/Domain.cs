namespace RKSoftware.Packages.ModelTransformer.Host;

public class Domain
{
    public string? StringOptional { get; set; }

    public required string StringRequired { get; set; }

    public string? StringIgnored { get; set; }
}
