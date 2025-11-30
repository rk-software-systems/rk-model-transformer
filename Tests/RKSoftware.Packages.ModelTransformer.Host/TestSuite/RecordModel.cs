namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

public record class RecordModel(string StringRequired, int IntRequired, string NullableStringToString)
{
    public string? StringOptional { get; init; }

    public int? IntOptional { get; init; }

    public string? StringIgnored { get; set; }

    public int? IntIgnored { get; set; }

    public string? StringMissed { get; set; }

    public int? IntMissed { get; set; }

    public string? StringReadonly { get; }

    public string? StringRequiredToNullableString { get; set; }

    public int? IntToNullableInt { get; set; }

    public List<string>? ListOfStrings { get; set; }
}
