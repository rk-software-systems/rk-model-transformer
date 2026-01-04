using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexNullableToNotNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ComplexNullableToNotNullableTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Industry = new IndustryDomain
            {
                Id = 13,
                Name = "Chemical"
            }
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Industry);

        Assert.Equal(domain.Industry.Id, viewModel.Industry.Id);
        Assert.Equal(domain.Industry.Name, viewModel.Industry.Name);
    }

    [Fact]
    public void TestCreateViewModelFromDomainWithNullChild()
    {
        var domain = new CompanyDomain
        {
            Industry = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.NotNull(viewModel.Industry);
        Assert.Equal(CompanyDomainExtensions.DefaultIndustry.Id, viewModel.Industry.Id);
        Assert.Equal(CompanyDomainExtensions.DefaultIndustry.Name, viewModel.Industry.Name);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Industry = new IndustryDomain
            {
                Id = 13,
                Name = "Chemical"
            }
        };

        var viewModel = new CompanyViewModel
        {
            Industry = new IndustryViewModel
            {
                Id = 27,
                Name = "Pharmaceutical"
            }
        };

        Assert.NotEqual(domain.Industry.Id, viewModel.Industry.Id);
        Assert.NotEqual(domain.Industry.Name, viewModel.Industry.Name);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Equal(domain.Industry.Id, viewModel.Industry.Id);
        Assert.Equal(domain.Industry.Name, viewModel.Industry.Name);
    }

    [Fact]
    public void TestUpdateViewModelFromDomainWithNullChild()
    {
        var domain = new CompanyDomain
        {
            Industry = null
        };

        var viewModel = new CompanyViewModel
        {
            Industry = new IndustryViewModel
            {
                Id = 27,
                Name = "Pharmaceutical"
            }
        };

        Assert.NotEqual(domain.Industry?.Id, viewModel.Industry.Id);
        Assert.NotEqual(domain.Industry?.Name, viewModel.Industry.Name);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.Industry);
    }
}
