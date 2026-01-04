using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveWithoutDefaultMapping;

[ModelTransformerRegistration<CompanyDomain, CompanyViewModel>(
    WithoutDefaultMapping = new[] {
        nameof(CompanyViewModel.Description), 
        nameof(CompanyViewModel.EstablishedYear)
    }
)]
public static class CompanyTransformerRegistration
{
}
