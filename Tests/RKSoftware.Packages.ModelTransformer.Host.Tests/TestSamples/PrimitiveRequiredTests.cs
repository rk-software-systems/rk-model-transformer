using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveRequired;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveRequiredTests
{
    private const string _name = "SuperCompany";
    private const int _id = 3;


    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Id = _id,
            Name = _name
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(_id, viewModel.Id);
        Assert.Equal(_name, viewModel.Name);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Id = _id,
            Name = _name
        };

        var viewModel = new CompanyViewModel
        {
            Id = 7,
            Name = "OldName"
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Equal(_id, viewModel.Id);
        Assert.Equal(_name, viewModel.Name);
    }
}
