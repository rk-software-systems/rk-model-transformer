using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveReadonly;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveReadonlyTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain();

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotEqual(domain.IndustryCode, viewModel.IndustryCode);
        Assert.NotEqual(domain.Nip, viewModel.Nip);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain();
       
        var viewModel = new CompanyViewModel();        

        Assert.NotEqual(domain.IndustryCode, viewModel.IndustryCode);
        Assert.NotEqual(domain.Nip, viewModel.Nip);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        // Values should not be changed as they are read only
        Assert.NotEqual(domain.IndustryCode, viewModel.IndustryCode);
        Assert.NotEqual(domain.Nip, viewModel.Nip);
    }
}
