namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

public class Domain
{
    public string? StringOptional { get; set; }

    public int? IntOptional { get; set; }

    public required string StringRequired { get; set; }

    public required int IntRequired { get; set; }

    public string? StringIgnored { get; set; }

    public int? IntIgnored { get; set; }

    public string? StringReadonly { get; }

    public string? StringToInt { get; set; }

    public string? NullableStringToString { get; set; }

    public required string StringRequiredToNullableString { get; set; }

    public int IntToNullableInt { get; set; }
}
