namespace RKSoftware.Packages.ModelTransformer.Host.RandomSample;

public class Dto
{
    public string? StringOptional { get; set; }

    public int? IntOptional { get; set; }

    public required string StringRequired { get; set; }

    public required int IntRequired { get; set; }

    public string? StringIgnored { get; set; }

    public int? IntIgnored { get; set; }

    public string? StringMissed { get; set; }

    public int? IntMissed { get; set; }

    public string? StringReadonly { get; }

    public string? StringRequiredToNullableString { get; set; }

    public int? IntToNullableInt { get; set; }

    public List<string>? ListOfStrings { get; set; }

    public string? WithoutDefaultMapping { get; set; }
}
