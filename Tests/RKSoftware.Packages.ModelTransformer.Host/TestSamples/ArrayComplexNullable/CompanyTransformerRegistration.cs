using RKSoftware.Packages.ModelTransformer.Attributes;

namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayComplexNullable;

[ModelTransformerRegistration<ProjectDomain, ProjectViewModel>]
[ModelTransformerRegistration<CompanyDomain, CompanyViewModel>]
public class CompanyTransformerRegistration
{
}
