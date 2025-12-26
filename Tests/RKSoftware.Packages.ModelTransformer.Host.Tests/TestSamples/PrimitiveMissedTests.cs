using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveMissed;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveMissedTests
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
        Assert.NotNull(viewModel.PrincipalName);
        Assert.NotNull(viewModel.ProjectCount);
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

        Assert.NotNull(viewModel.PrincipalName);
        Assert.NotEqual(initPrincipalName, viewModel.PrincipalName);
        Assert.NotNull(viewModel.ProjectCount);
        Assert.NotEqual(initProjectCount, viewModel.ProjectCount);
    }
}
