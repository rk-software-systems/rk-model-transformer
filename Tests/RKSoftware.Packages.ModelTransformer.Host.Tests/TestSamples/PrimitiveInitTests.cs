using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveInit;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveInitTests
{
    private const string _createdBy = "Mr Creator";
    private readonly DateTime _createdOn = new(1993, 1,2);


    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            CreatedBy = _createdBy,
            CreatedOn = _createdOn
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(_createdBy, viewModel.CreatedBy);
        Assert.Equal(_createdOn, viewModel.CreatedOn);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            CreatedBy = _createdBy,
            CreatedOn = _createdOn
        };

        var viewModel = new CompanyViewModel
        {
            CreatedBy = "Another Creator",
            CreatedOn = new DateTime(2000, 1, 1)
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        // Values should not be changed as they are init only
        Assert.NotEqual(_createdBy, viewModel.CreatedBy);
        Assert.NotEqual(_createdOn, viewModel.CreatedOn);
    }
}
