using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayComplexNotNullableToNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ArrayComplexNotNullableToNullableTests
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

    private static ProjectDomain[] GetDomains()
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

    private static ProjectViewModel?[] GetViewModels()
    {
        return
            [
                new()
                {
                    Id = 2,
                    Name = "Project C"
                },
                new()
                {
                    Id = 3,
                    Name = "Project D"
                },
                null
            ];
    }

    private static void AssertEqualDomainsAndViewModels(ProjectDomain?[] domains, ProjectViewModel?[] viewModels)
    {
        domains = domains.Where(x => x != null).ToArray();
        var count = domains.Length;
        Assert.Equal(count, viewModels.Length);
        for (int i = 0; i < count; i++)
        {
            Assert.Equal(domains[i]?.Id, viewModels[i]?.Id);
            Assert.Equal(domains[i]?.Name, viewModels[i]?.Name);
        }
    }

    private static void AssertNotEqualDomainsAndViewModels(ProjectDomain?[] domains, ProjectViewModel?[] viewModels)
    {
        var count = Math.Min(domains.Length, viewModels.Length);        
        for (int i = 0; i < count; i++)
        {
            Assert.NotEqual(domains[i]?.Id, viewModels[i]?.Id);
            Assert.NotEqual(domains[i]?.Name, viewModels[i]?.Name);
        }
    }

    #endregion
}
