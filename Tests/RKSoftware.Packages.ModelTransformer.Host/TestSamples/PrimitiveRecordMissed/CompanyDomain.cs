namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRecordMissed;

public record class CompanyDomain(int Id)
{
    public string? Description { get; set; }
}
