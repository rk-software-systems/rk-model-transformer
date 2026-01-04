using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveWithoutDefaultMapping;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveWithoutDefaultMappingTests
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

        Assert.NotEqual(_description, viewModel.Description);
        Assert.NotEqual(_establishedYear, viewModel.EstablishedYear);

        Assert.Equal(CompanyDomainExtensions.Description, viewModel.Description);
        Assert.Equal(CompanyDomainExtensions.EstablishedYear, viewModel.EstablishedYear);
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

        Assert.NotEqual(_description, viewModel.Description);
        Assert.NotEqual(_establishedYear, viewModel.EstablishedYear);

        Assert.Equal(CompanyDomainExtensions.Description, viewModel.Description);
        Assert.Equal(CompanyDomainExtensions.EstablishedYear, viewModel.EstablishedYear);
    }
}
