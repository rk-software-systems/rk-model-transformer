using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexStructureNotNullableToNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ComplexStructureNotNullableToNullableTests
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
        Assert.NotNull(viewModel.Industry);

        Assert.Equal(domain.Industry.Id, viewModel.Industry?.Id);
        Assert.Equal(domain.Industry.Name, viewModel.Industry?.Name);
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

        Assert.NotEqual(domain.Industry.Id, viewModel.Industry?.Id);
        Assert.NotEqual(domain.Industry.Name, viewModel.Industry?.Name);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Equal(domain.Industry.Id, viewModel.Industry?.Id);
        Assert.Equal(domain.Industry.Name, viewModel.Industry?.Name);
    }
}
