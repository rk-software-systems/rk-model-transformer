using RKSoftware.Packages.ModelTransformer.Host.TestSamples.PrimitiveImplicit;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class PrimitiveImplicitTests
{
    private const int _numberOfDepartments = 10;
    private const int _numberOfAccountants = 5;
    private const float _averageOfRanking = 35.57f;
    private const decimal _averageOfSalary = 55000.75m;


    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            NumberOfDepartments = _numberOfDepartments,
            NumberOfAccountants = _numberOfAccountants,
            AverageOfRanking = _averageOfRanking,
            AverageOfSalary = _averageOfSalary
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(_numberOfDepartments, viewModel.NumberOfDepartments);
        Assert.Equal((short)_numberOfAccountants, viewModel.NumberOfAccountants);
        Assert.Equal(_averageOfRanking, viewModel.AverageOfRanking);
        Assert.Equal((double)_averageOfSalary, viewModel.AverageOfSalary);
    }

    [Fact]
    public void TestCreateViewModelFromDomainWithNull()
    {
        var domain = new CompanyDomain
        {
            NumberOfDepartments = null,
            NumberOfAccountants = null,
            AverageOfRanking = null,
            AverageOfSalary = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Null(viewModel.NumberOfDepartments);
        Assert.Null(viewModel.NumberOfAccountants);
        Assert.Null(viewModel.AverageOfRanking);
        Assert.Null(viewModel.AverageOfSalary);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            NumberOfDepartments = _numberOfDepartments,
            NumberOfAccountants = _numberOfAccountants,
            AverageOfRanking = _averageOfRanking,
            AverageOfSalary = _averageOfSalary
        };

        var viewModel = new CompanyViewModel
        {
            NumberOfDepartments = 37,
            NumberOfAccountants = 96,
            AverageOfRanking = 3.3,
            AverageOfSalary = 12.23
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Equal(_numberOfDepartments, viewModel.NumberOfDepartments);
        Assert.Equal((short)_numberOfAccountants, viewModel.NumberOfAccountants);
        Assert.Equal(_averageOfRanking, viewModel.AverageOfRanking);
        Assert.Equal((double)_averageOfSalary, viewModel.AverageOfSalary);
    }

    [Fact]
    public void TestUpdateViewModelFromDomainWithNull()
    {
        var domain = new CompanyDomain
        {
            NumberOfDepartments = null,
            NumberOfAccountants = null,
            AverageOfRanking = null,
            AverageOfSalary = null
        };
        var viewModel = new CompanyViewModel
        {
            NumberOfDepartments = 37,
            NumberOfAccountants = 96,
            AverageOfRanking = 3.3,
            AverageOfSalary = 12.23
        };
        var updatedViewModel = domain.Transform(viewModel);
        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.NumberOfDepartments);
        Assert.Null(viewModel.NumberOfAccountants);
        Assert.Null(viewModel.AverageOfRanking);
        Assert.Null(viewModel.AverageOfSalary);
    }
}
