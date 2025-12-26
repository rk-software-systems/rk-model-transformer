using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveReadonlyToOptional;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveReadonlyToOptionalTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain();

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(domain.IndustryCode, viewModel.IndustryCode);
        Assert.Equal(domain.Nip, viewModel.Nip);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain();

        var viewModel = new CompanyViewModel();

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        Assert.Equal(domain.IndustryCode, viewModel.IndustryCode);
        Assert.Equal(domain.Nip, viewModel.Nip);
    }
}
