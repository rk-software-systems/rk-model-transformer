using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitivePostDefault;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitivePostDefaultTests
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

        // Check that the original values are not retained
        Assert.NotEqual(_description, viewModel.Description);
        Assert.NotEqual(_establishedYear, viewModel.EstablishedYear);

        // Check that the overridden default values are applied
        Assert.Equal(CompanyDomainExtensions.OverriddenDescription, viewModel.Description);
        Assert.Equal(CompanyDomainExtensions.OverriddenEstablishedYear, viewModel.EstablishedYear);
    }

    [Fact]
    public void TestCreateViewModelFromDomainWithNull()
    {
        var domain = new CompanyDomain
        {
            Description = null,
            EstablishedYear = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        // Check that the original values are not retained
        Assert.NotNull(viewModel.Description);
        Assert.NotNull(viewModel.EstablishedYear);

        // Check that the overridden default values are applied
        Assert.Equal(CompanyDomainExtensions.OverriddenDescription, viewModel.Description);
        Assert.Equal(CompanyDomainExtensions.OverriddenEstablishedYear, viewModel.EstablishedYear);
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

        // Check that the original values are not retained
        Assert.NotEqual(_description, viewModel.Description);
        Assert.NotEqual(_establishedYear, viewModel.EstablishedYear);

        // Check that the overridden default values are applied
        Assert.Equal(CompanyDomainExtensions.OverriddenDescription, updatedViewModel.Description);
        Assert.Equal(CompanyDomainExtensions.OverriddenEstablishedYear, updatedViewModel.EstablishedYear);
    }

    [Fact]
    public void TestUpdateViewModelFromDomainWithNull()
    {
        var domain = new CompanyDomain
        {
            Description = null,
            EstablishedYear = null
        };

        var viewModel = new CompanyViewModel
        {
            Description = null,
            EstablishedYear = null
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        // Check that the original values are not retained
        Assert.NotNull(viewModel.Description);
        Assert.NotNull(viewModel.EstablishedYear);

        // Check that the overridden default values are applied
        Assert.Equal(CompanyDomainExtensions.OverriddenDescription, updatedViewModel.Description);
        Assert.Equal(CompanyDomainExtensions.OverriddenEstablishedYear, updatedViewModel.EstablishedYear);
    }
}
