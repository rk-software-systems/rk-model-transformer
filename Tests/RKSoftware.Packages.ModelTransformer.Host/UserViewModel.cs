namespace RKSoftware.Packages.ModelTransformer.Host;

public class UserViewModel
{
    public string? StringOptional { get; set; }

    public required string StringRequired { get; set; }

    public string? StringIgnored { get; set; }
}
