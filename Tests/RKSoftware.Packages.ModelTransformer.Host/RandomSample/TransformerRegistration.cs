using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.RandomSample;

[ModelTransformerRegistration<DomainChild, ViewModelChild>]
[ModelTransformerRegistration<Domain, RecordModel>(
    Ignored = [nameof(ViewModel.StringIgnored), nameof(ViewModel.IntIgnored), nameof(ViewModel.StringReadonly)],
    WithoutDefaultMapping = [nameof(ViewModel.WithoutDefaultMapping)]
)]
[ModelTransformerRegistration<Domain, Dto>(
    Ignored = [nameof(ViewModel.StringIgnored), nameof(ViewModel.IntIgnored), nameof(ViewModel.StringReadonly)],
    WithoutDefaultMapping = [nameof(ViewModel.WithoutDefaultMapping)]
)]
[ModelTransformerRegistration<Domain, ViewModel>(
    Ignored = [
        nameof(ViewModel.StringIgnored), 
        nameof(ViewModel.IntIgnored), 
        nameof(ViewModel.StringReadonly)
    ],
    WithoutDefaultMapping = [nameof(ViewModel.WithoutDefaultMapping)]
)]
public class TransformerRegistration
{
}
