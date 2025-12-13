namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

public class ViewModel
{
    public string? StringOptional { get; set; }

    public int? IntOptional { get; set; }

    public required string StringRequired { get; set; }

    public required int IntRequired { get; set; }

    public string? StringIgnored { get; set; }

    public int? IntIgnored { get; set; }

    public required string StringRequiredIgnored { get; set; }

    public required int IntRequiredIgnored { get; set; }

    public string? StringMissed { get; set; }

    public int? IntMissed { get; set; }

    public string? StringReadonly { get; }

    public int? StringToInt { get; set; }

    public required string NullableStringToString { get; set; }

    public string? StringRequiredToNullableString { get; set; }

    public int? IntToNullableInt { get; set; }

    public List<string>? ListOfStrings { get; set; }

    public ViewModelChild? Child { get; set; }

    public List<ViewModelChild>? Children { get; set; }
}
