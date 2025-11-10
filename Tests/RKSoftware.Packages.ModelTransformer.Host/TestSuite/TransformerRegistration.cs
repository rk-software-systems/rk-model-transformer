using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

[ModelTransformerRegistration<Domain, RecordModel>(
    MethodName = "ToRecord",
    IgnoredProperties = [nameof(ViewModel.StringIgnored), nameof(ViewModel.IntIgnored), nameof(ViewModel.StringReadonly)])]
[ModelTransformerRegistration<Domain, Dto>(
    MethodName = "ToDto",
    IgnoredProperties = [nameof(ViewModel.StringIgnored), nameof(ViewModel.IntIgnored), nameof(ViewModel.StringReadonly)])]
[ModelTransformerRegistration<Domain, ViewModel>(
    IgnoredProperties = [nameof(ViewModel.StringIgnored), nameof(ViewModel.IntIgnored), nameof(ViewModel.StringReadonly)])]
public class TransformerRegistration
{
}
