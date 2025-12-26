using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveIgnored;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveIgnoredTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            LastUpdatedBy = "Admin",
            LastUpdatedOn = new DateTime(2024, 1, 1)
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Null(viewModel.LastUpdatedBy);
        Assert.Null(viewModel.LastUpdatedOn);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            LastUpdatedBy = "Admin",
            LastUpdatedOn = new DateTime(2024, 1, 1)
        };

        var viewModel = new CompanyViewModel
        {
            LastUpdatedBy = "User",
            LastUpdatedOn = new DateTime(2023, 12, 31)
        };

        Assert.NotEqual(domain.LastUpdatedBy, viewModel.LastUpdatedBy);
        Assert.NotEqual(domain.LastUpdatedOn, viewModel.LastUpdatedOn);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        // Values should not be changed as they are ignored
        Assert.NotNull(viewModel.LastUpdatedOn);
        Assert.NotEqual(domain.LastUpdatedOn, viewModel.LastUpdatedOn);
        Assert.NotNull(viewModel.LastUpdatedBy);
        Assert.NotEqual(domain.LastUpdatedBy, viewModel.LastUpdatedBy);
    }
}
