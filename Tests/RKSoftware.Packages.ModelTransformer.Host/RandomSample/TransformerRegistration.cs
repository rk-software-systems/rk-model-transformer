using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.RandomSample;

[ModelTransformerRegistration<DomainChild, ViewModelChild>]
[ModelTransformerRegistration<Domain, RecordModel>(
    IgnoredProperties = [nameof(ViewModel.StringIgnored), nameof(ViewModel.IntIgnored), nameof(ViewModel.StringReadonly)])]
[ModelTransformerRegistration<Domain, Dto>(
    IgnoredProperties = [nameof(ViewModel.StringIgnored), nameof(ViewModel.IntIgnored), nameof(ViewModel.StringReadonly)])]
[ModelTransformerRegistration<Domain, ViewModel>(
    IgnoredProperties = [
        nameof(ViewModel.StringIgnored), 
        nameof(ViewModel.IntIgnored), 
        nameof(ViewModel.StringReadonly)
    ])]
public class TransformerRegistration
{
}
