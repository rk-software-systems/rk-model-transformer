using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexStructureNullableToNotNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ComplexStructureNullableToNotNullableTests
{
    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var industry = new IndustryModel
        {
            Id = 13,
            Name = "Chemical"
        };

        var domain = new CompanyDomain
        {
            Industry = industry
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Equal(domain.Industry?.Id, viewModel.Industry.Id);
        Assert.Equal(domain.Industry?.Name, viewModel.Industry.Name);
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
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Industry = new IndustryModel
            {
                Id = 13,
                Name = "Chemical"
            }
        };

        var viewModel = new CompanyViewModel
        {
            Industry = new IndustryModel
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
        Assert.Equal(domain.Industry?.Id, viewModel.Industry.Id);
        Assert.Equal(domain.Industry?.Name, viewModel.Industry.Name);
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
            Industry = new IndustryModel
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
    }
}
