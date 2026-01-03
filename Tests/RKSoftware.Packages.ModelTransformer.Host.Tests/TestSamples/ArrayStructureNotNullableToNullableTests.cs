using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayStructureNotNullableToNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ArrayStructureNotNullableToNullableTests
{
    [Fact]
    public void TestCreateViewModelToArray()
    {
        var domain = new CompanyDomain
        {
            Projects = GetDomains()
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Projects);

        AssertEqualDomainsAndViewModels(domain.Projects, viewModel.Projects);
    }

    [Fact]
    public void TestCreateViewModelToArrayFromNull()
    {
        var domain = new CompanyDomain
        {
            Projects = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.Projects);
    }

    [Fact]
    public void TestUpdateViewModelToArray()
    {
        var domain = new CompanyDomain
        {
            Projects = GetDomains()
        };

        var viewModel = new CompanyViewModel
        {
            Projects = GetViewModels()
        };

        AssertNotEqualDomainsAndViewModels(domain.Projects, viewModel.Projects);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.Projects);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.Projects, viewModel.Projects);
    }

    [Fact]
    public void TestUpdateViewModelToArrayFromNull()
    {
        var domain = new CompanyDomain
        {
            Projects = null
        };

        var viewModel = new CompanyViewModel
        {
            Projects = GetViewModels()
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.Projects);
    }

    #region helpers

    private static ProjectModel[] GetDomains()
    {
        return
            [
                new()
                {
                    Id = 1,
                    Name = "Project A"
                },
                new()
                {
                    Id = 2,
                    Name = "Project B"
                }
            ];
    }

    private static ProjectModel?[] GetViewModels()
    {
        return
            [
                new()
                {
                    Id = 2,
                    Name = "Project C"
                },
                null,
                new()
                {
                    Id = 3,
                    Name = "Project D"
                }                
            ];
    }

    private static void AssertEqualDomainsAndViewModels(IEnumerable<ProjectModel> domains, IEnumerable<ProjectModel?> viewModels)
    {
        var count = domains.Count();
        Assert.Equal(count, viewModels.Count());
        for (int i = 0; i < count; i++)
        {
            Assert.Equal(domains.ElementAt(i).Id, viewModels.ElementAt(i)?.Id);
            Assert.Equal(domains.ElementAt(i).Name, viewModels.ElementAt(i)?.Name);
        }
    }

    private static void AssertNotEqualDomainsAndViewModels(IEnumerable<ProjectModel> domains, IEnumerable<ProjectModel?> viewModels)
    {
        var count = Math.Min(domains.Count(), viewModels.Count());
        for (int i = 0; i < count; i++)
        {
            Assert.NotEqual(domains.ElementAt(i).Id, viewModels.ElementAt(i)?.Id);
            Assert.NotEqual(domains.ElementAt(i).Name, viewModels.ElementAt(i)?.Name);
        }
    }

    #endregion
}
