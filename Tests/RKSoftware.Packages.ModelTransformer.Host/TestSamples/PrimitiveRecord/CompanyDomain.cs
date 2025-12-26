namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRecord;

public record class CompanyDomain(int Id, string Name)
{
    public string? Description { get; set; }

    public int? ParentCompanyId { get; set; }
}
