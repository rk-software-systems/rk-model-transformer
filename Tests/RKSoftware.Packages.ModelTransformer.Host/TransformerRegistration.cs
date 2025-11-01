using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host;

[ModelTransformerRegistration<User, UserViewModel>(nameof(UserViewModel.StringIgnored))]
public class TransformerRegistration
{
}
