using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveOptionalToRequired;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveOptionalToRequiredTests
{
    private const string? _title = null;
    private readonly int? _brandId = null;


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
        Assert.Equal(default, viewModel.Title);
        Assert.Equal(default, viewModel.BrandId);
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
            Title = "Med Corp",
            BrandId = 5
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Equal(default, viewModel.Title);
        Assert.Equal(default, viewModel.BrandId);
    }
}
