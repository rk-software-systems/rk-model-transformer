namespace RKSoftware.Packages.ModelTransformer.Host.MagazineSample;

public class SourceModel : BaseSourceModel
{
    public long Id { get; set; }

    public required string Part1 { get; set; }

    public required string Part2 { get; set; }

    public required string CurrentName { get; set; }

    public string? DoNotMap { get; set; }

    public int NotMappedInSource { get; set; }

    public required SourceNestedModel Nested { get; set; }

    public required string EnumProperty { get; set; }

    public required List<SourceListItem> Items { get; set; }

    public required Dictionary<int, string> Dict { get; set; }
}
