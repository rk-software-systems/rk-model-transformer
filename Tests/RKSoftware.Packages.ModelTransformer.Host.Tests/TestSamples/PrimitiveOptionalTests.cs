using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveOptional;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveOptionalTests
{
    private const string _description = "A sample company";
    private const int _establishedYear = 1990;


    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Description = _description,
            EstablishedYear = _establishedYear
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(_description, viewModel.Description);
        Assert.Equal(_establishedYear, viewModel.EstablishedYear);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Description = _description,
            EstablishedYear = _establishedYear
        };

        var viewModel = new CompanyViewModel
        {
            Description = "Old Description",
            EstablishedYear = 2000
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Equal(_description, viewModel.Description);
        Assert.Equal(_establishedYear, viewModel.EstablishedYear);
    }
}
