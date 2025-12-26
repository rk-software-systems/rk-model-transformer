using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRecord;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveRecordTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain(1, "Test Company")
        {
            Description = "A company for testing purposes",
            ParentCompanyId = 23
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(domain.Id, viewModel.Id);
        Assert.Equal(domain.Name, viewModel.Name);
        Assert.Equal(domain.Description, viewModel.Description);
        Assert.Equal(domain.ParentCompanyId, viewModel.ParentCompanyId);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain(1, "Sport Company")
        {
            Description = "The company provides sporting goods",
            ParentCompanyId = 23
        };

        var viewModel = new CompanyViewModel(2, "Chemical Company")
        {
            Description = "The company produces chemical products",
            ParentCompanyId = 35
        };

        Assert.NotEqual(domain.Description, viewModel.Description);
        Assert.NotEqual(domain.ParentCompanyId, viewModel.ParentCompanyId);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        // Values should not be changed as they are read only
        Assert.NotEqual(domain.Id, viewModel.Id);
        Assert.NotEqual(domain.Name, viewModel.Name);

        // Values should be updated
        Assert.Equal(domain.Description, viewModel.Description);
        Assert.Equal(domain.ParentCompanyId, viewModel.ParentCompanyId);
    }
}
