using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayComplexNullableToNotNullable;

[ModelTransformerRegistration<ProjectDomain, ProjectViewModel>]
[ModelTransformerRegistration<CompanyDomain, CompanyViewModel>]
public class CompanyTransformerRegistration
{
}
