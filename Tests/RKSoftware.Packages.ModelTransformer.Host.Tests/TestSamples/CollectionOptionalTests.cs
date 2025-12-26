using RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionOptional;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class CollectionOptionalTests
{
    [Fact]
    public void TestCreateViewModelFromDomainList()
    {
        var domain = new CompanyDomain
        {
            ProjectList = GetDomains()
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectList);

        AssertEqualDomainsAndViewModels(domain.ProjectList, viewModel.ProjectList);
    }

    [Fact]
    public void TestCreateViewModelFromDomainListWithNullChild()
    {
        var domain = new CompanyDomain
        {
            ProjectList = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectList);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            ProjectList = GetDomains()
        };

        var viewModel = new CompanyViewModel
        {
            ProjectList = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectList, viewModel.ProjectList);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectList);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectList, viewModel.ProjectList);
    }

    [Fact]
    public void TestUpdateViewModelFromDomainWithNullChild()
    {
        var domain = new CompanyDomain
        {
            ProjectList = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectList = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectList);
    }

    #region helpers

    private static List<ProjectDomain> GetDomains()
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

    private static IEnumerable<ProjectViewModel> GetViewModels()
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
                }
            ];
    }

    private static void AssertEqualDomainsAndViewModels(
        IEnumerable<ProjectDomain> domains,
        IEnumerable<ProjectViewModel> viewModels)
    {
        var count = domains.Count();
        Assert.Equal(count, viewModels.Count());
        for (int i = 0; i < count; i++)
        {
            Assert.Equal(domains.ElementAt(i).Id, viewModels.ElementAt(i).Id);
            Assert.Equal(domains.ElementAt(i).Name, viewModels.ElementAt(i).Name);
        }
    }

    private static void AssertNotEqualDomainsAndViewModels(
        IEnumerable<ProjectDomain> domains,
        IEnumerable<ProjectViewModel> viewModels)
    {
        var count = Math.Min(domains.Count(), viewModels.Count());        
        for (int i = 0; i < count; i++)
        {
            Assert.NotEqual(domains.ElementAt(i).Id, viewModels.ElementAt(i).Id);
            Assert.NotEqual(domains.ElementAt(i).Name, viewModels.ElementAt(i).Name);
        }
    }

    #endregion
}
