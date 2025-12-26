using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRecordMissed;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveRecordMissedTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain(1)
        {
            Description = "A company for testing purposes"
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(domain.Id, viewModel.Id);
        Assert.NotNull(viewModel.Name);
        Assert.Equal(domain.Description, viewModel.Description);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain(1)
        {
            Description = "The company provides sporting goods"
        };

        var initName = "Chemical Company";
        var viewModel = new CompanyViewModel(2, initName)
        {
            Description = "The company produces chemical products"
        };

        Assert.NotEqual(domain.Description, viewModel.Description);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        // Values should not be changed as they are read only
        Assert.NotEqual(domain.Id, viewModel.Id);
        Assert.Equal(initName, viewModel.Name);

        // Values should be updated
        Assert.Equal(domain.Description, viewModel.Description);
    }
}
