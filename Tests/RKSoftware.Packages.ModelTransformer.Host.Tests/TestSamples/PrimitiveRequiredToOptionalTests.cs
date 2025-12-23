using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRequiredToOptional;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveRequiredToOptionalTests
{
    private const string _title = "Software Solutions";
    private readonly int _brandId = 9;


    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Title = _title,
            BrandId = _brandId
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Title);
        Assert.Equal(_title, viewModel.Title);
        Assert.NotNull(viewModel.BrandId);
        Assert.Equal(_brandId, viewModel.BrandId);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Title = _title,
            BrandId = _brandId
        };

        var viewModel = new CompanyViewModel
        {
            Title = null,
            BrandId = null
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.NotNull(updatedViewModel.Title);
        Assert.Equal(_title, updatedViewModel.Title);
        Assert.NotNull(updatedViewModel.BrandId);
        Assert.Equal(_brandId, updatedViewModel.BrandId);
    }
}
