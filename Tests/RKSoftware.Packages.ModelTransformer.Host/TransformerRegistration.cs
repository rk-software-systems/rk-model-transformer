using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host;

[ModelTransformerRegistration<Domain, ViewModel>(nameof(ViewModel.StringIgnored))]
public class TransformerRegistration
{
}
