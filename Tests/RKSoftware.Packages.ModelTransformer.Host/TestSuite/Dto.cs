namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

public class Dto
{
    public string? StringOptional { get; set; }

    public required string StringRequired { get; set; }

    public string? StringIgnored { get; set; }

    public string? StringMissed { get; set; }
}
