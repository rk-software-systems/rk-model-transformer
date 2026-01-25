using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.MagazineSample;

[ModelTransformerRegistration<SourceListItem, DestinationListItem>]
[ModelTransformerRegistration<SourceNestedModel, DestinationNestedModel>]
[ModelTransformerRegistration<SourceModel, DestinationModel>(
    Ignored = [nameof(DestinationModel.DoNotMap), nameof(DestinationModel.NotMappedInDestination)],
    WithoutDefaultMapping = [nameof(DestinationModel.SerialNumber)]
)]
public class SourceModelTransformerRegistration
{
}
