using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSuite;

[ModelTransformerRegistration<Domain, ViewModelWithConstructor>(MethodName = "ToViewModelWithConstructor")]
[ModelTransformerRegistration<Domain, Dto>(MethodName = "ToDto", IgnoredProperties = [nameof(ViewModel.StringIgnored)])]
[ModelTransformerRegistration<Domain, ViewModel>(IgnoredProperties = [nameof(ViewModel.StringIgnored)])]
public class TransformerRegistration
{
}
