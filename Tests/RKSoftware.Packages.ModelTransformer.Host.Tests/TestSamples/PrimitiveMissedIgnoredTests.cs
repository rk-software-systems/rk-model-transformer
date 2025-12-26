using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveMissedIgnored;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveMissedIgnoredTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Id = 1,
            Name = "Test Company"
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Null(viewModel.PrincipalName);
        Assert.Null(viewModel.ProjectCount);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Id = 1,
            Name = "Test Company"
        };

        var initPrincipalName = "Mr First";
        var initProjectCount = 10;
        var viewModel = new CompanyViewModel
        {
            PrincipalName = initPrincipalName,
            ProjectCount = initProjectCount
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        // Values should not be changed as they are ignored
        Assert.NotNull(viewModel.PrincipalName);
        Assert.Equal(initPrincipalName, viewModel.PrincipalName);
        Assert.NotNull(viewModel.ProjectCount);
        Assert.Equal(initProjectCount, viewModel.ProjectCount);
    }
}
