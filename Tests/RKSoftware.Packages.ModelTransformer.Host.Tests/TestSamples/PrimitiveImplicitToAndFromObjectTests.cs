using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveImplicitToAndFromObject;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveImplicitToAndFromObjectTests
{
    private const string _description = "A sample company";
    private const int _establishedYear = 1990;
    private readonly object _name = "Sample Company Inc.";
    private readonly object _employeeCount = 150;

    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Description = _description,
            EstablishedYear = _establishedYear,
            Name = _name,
            EmployeeCount = _employeeCount
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(_description, viewModel.Description);
        Assert.Equal(_establishedYear, viewModel.EstablishedYear);
        Assert.Equal(_name, viewModel.Name);
        Assert.Equal(_employeeCount, viewModel.EmployeeCount);
    }

    [Fact]
    public void TestCreateViewModelFromDomainWithNull()
    {
        var domain = new CompanyDomain
        {
            Description = null,
            EstablishedYear = null,
            Name = null,
            EmployeeCount = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Null(viewModel.Description);
        Assert.Null(viewModel.EstablishedYear);
        Assert.Null(viewModel.Name);
        Assert.Null(viewModel.EmployeeCount);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Description = _description,
            EstablishedYear = _establishedYear,
            Name = _name,
            EmployeeCount = _employeeCount
        };

        var viewModel = new CompanyViewModel
        {
            Description = "A test company",
            EstablishedYear = 2096,
            Name = "",
            EmployeeCount = 13
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Equal(_description, viewModel.Description);
        Assert.Equal(_establishedYear, viewModel.EstablishedYear);
        Assert.Equal(_name, viewModel.Name);
        Assert.Equal(_employeeCount, viewModel.EmployeeCount);
    }

    [Fact]
    public void TestUpdateViewModelFromDomainWithNull()
    {
        var domain = new CompanyDomain
        {
            Description = null,
            EstablishedYear = null,
            Name = null,
            EmployeeCount = null
        };
        var viewModel = new CompanyViewModel
        {
            Description = "A test company",
            EstablishedYear = 2096,
            Name = "",
            EmployeeCount = 13
        };
        var updatedViewModel = domain.Transform(viewModel);
        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.Description);
        Assert.Null(viewModel.EstablishedYear);
        Assert.Null(viewModel.Name);
        Assert.Null(viewModel.EmployeeCount);
    }
}
