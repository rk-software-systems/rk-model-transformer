namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRecordMissed;

public record class CompanyViewModel(int Id, string Name)
{
    public string? Description { get; set; }
}
