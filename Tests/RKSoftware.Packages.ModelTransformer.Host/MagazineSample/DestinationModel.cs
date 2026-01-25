namespace RKSoftware.Packages.ModelTransformer.Host.MagazineSample;

public class DestinationModel : BaseDestinationModel
{
    public long Id { get; set; }

    public required string Combined { get; set; }

    public long ConstValue { get; set; }

    public bool EvaluatedFromExternal { get; set; }

    public required string DifferentName { get; set; }

    public string? DoNotMap { get; set; }

    public int? NotMappedInDestination { get; set; }

    public required DestinationNestedModel Nested { get; set; }

    public required AddressKinds EnumProperty { get; set; }

    public required List<DestinationListItem> Items { get; set; }

    public required Dictionary<int, string> Dict { get; set; }
}
