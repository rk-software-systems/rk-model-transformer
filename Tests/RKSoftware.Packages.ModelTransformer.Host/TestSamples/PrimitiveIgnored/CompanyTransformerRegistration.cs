using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveIgnored;

[ModelTransformerRegistration<CompanyDomain, CompanyViewModel>(
    IgnoredProperties = new[] {
        nameof(CompanyViewModel.LastUpdatedBy), 
        nameof(CompanyViewModel.LastUpdatedOn)
    }
)]
public class CompanyTransformerRegistration
{
}
