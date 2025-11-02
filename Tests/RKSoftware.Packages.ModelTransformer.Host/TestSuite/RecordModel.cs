namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

public record class RecordModel(string StringRequired)
{
    public string? StringOptional { get; init; }
}
