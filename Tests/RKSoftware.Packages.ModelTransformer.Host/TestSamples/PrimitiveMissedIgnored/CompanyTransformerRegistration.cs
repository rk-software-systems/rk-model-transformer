using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveMissedIgnored;

[ModelTransformerRegistration<CompanyDomain, CompanyViewModel>(
    IgnoredProperties = new[] {
        nameof(CompanyViewModel.PrincipalName), 
        nameof(CompanyViewModel.ProjectCount)
    }
)]
public class CompanyTransformerRegistration
{
}
