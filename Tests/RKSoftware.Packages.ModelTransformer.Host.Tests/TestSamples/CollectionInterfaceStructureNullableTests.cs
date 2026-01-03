using RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfaceStructureNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class CollectionInterfaceStructureNullableTests
{
    #region IList

    [Fact]
    public void TestCreateViewModelToIList()
    {
        var domain = new CompanyDomain
        {
            ProjectIList = GetDomains()
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIList);

        AssertEqualDomainsAndViewModels(domain.ProjectIList, viewModel.ProjectIList);
    }

    [Fact]
    public void TestCreateViewModelToIListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIList = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectIList);
    }

    [Fact]
    public void TestUpdateViewModelToIList()
    {
        var domain = new CompanyDomain
        {
            ProjectIList = GetDomains()
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIList = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIList, viewModel.ProjectIList);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIList);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectIList, viewModel.ProjectIList);
    }

    [Fact]
    public void TestUpdateViewModelToIListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIList = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIList = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIList);
    }

    #endregion

    #region ICollection

    [Fact]
    public void TestCreateViewModelToICollection()
    {
        var domain = new CompanyDomain
        {
            ProjectICollection = GetDomains()
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectICollection);

        AssertEqualDomainsAndViewModels(domain.ProjectICollection, viewModel.ProjectICollection);
    }

    [Fact]
    public void TestCreateViewModelToICollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectICollection = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectICollection);
    }

    [Fact]
    public void TestUpdateViewModelToICollection()
    {
        var domain = new CompanyDomain
        {
            ProjectICollection = GetDomains()
        };

        var viewModel = new CompanyViewModel
        {
            ProjectICollection = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectICollection, viewModel.ProjectICollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectICollection);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectICollection, viewModel.ProjectICollection);
    }

    [Fact]
    public void TestUpdateViewModelToICollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectICollection = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectICollection = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectICollection);
    }

    #endregion

    #region IEnumerable

    [Fact]
    public void TestCreateViewModelToIEnumerable()
    {
        var domain = new CompanyDomain
        {
            ProjectIEnumerable = GetDomains()
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIEnumerable);

        AssertEqualDomainsAndViewModels(domain.ProjectIEnumerable, viewModel.ProjectIEnumerable);
    }

    [Fact]
    public void TestCreateViewModelToIEnumerableFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIEnumerable = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectIEnumerable);
    }

    [Fact]
    public void TestUpdateViewModelToIEnumerable()
    {
        var domain = new CompanyDomain
        {
            ProjectIEnumerable = GetDomains()
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIEnumerable = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIEnumerable, viewModel.ProjectIEnumerable);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIEnumerable);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectIEnumerable, viewModel.ProjectIEnumerable);
    }

    [Fact]
    public void TestUpdateViewModelToIEnumerableFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIEnumerable = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIEnumerable = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIEnumerable);
    }

    #endregion

    #region IReadOnlyCollection

    [Fact]
    public void TestCreateViewModelToIReadOnlyCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyCollection = GetDomains()
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIReadOnlyCollection);

        AssertEqualDomainsAndViewModels(domain.ProjectIReadOnlyCollection, viewModel.ProjectIReadOnlyCollection);
    }

    [Fact]
    public void TestCreateViewModelToIReadOnlyCollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyCollection = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectIReadOnlyCollection);
    }

    [Fact]
    public void TestUpdateViewModelToIReadOnlyCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyCollection = GetDomains()
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIReadOnlyCollection = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIReadOnlyCollection, viewModel.ProjectIReadOnlyCollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIReadOnlyCollection);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectIReadOnlyCollection, viewModel.ProjectIReadOnlyCollection);
    }

    [Fact]
    public void TestUpdateViewModelToIReadOnlyCollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyCollection = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIReadOnlyCollection = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIReadOnlyCollection);
    }

    #endregion

    #region IReadOnlyList

    [Fact]
    public void TestCreateViewModelToIReadOnlyList()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyList = GetDomains()
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIReadOnlyList);

        AssertEqualDomainsAndViewModels(domain.ProjectIReadOnlyList, viewModel.ProjectIReadOnlyList);
    }

    [Fact]
    public void TestCreateViewModelToIReadOnlyListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyList = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectIReadOnlyList);
    }

    [Fact]
    public void TestUpdateViewModelToIReadOnlyList()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyList = GetDomains()
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIReadOnlyList = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIReadOnlyList, viewModel.ProjectIReadOnlyList);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIReadOnlyList);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectIReadOnlyList, viewModel.ProjectIReadOnlyList);
    }

    [Fact]
    public void TestUpdateViewModelToIReadOnlyListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyList = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIReadOnlyList = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIReadOnlyList);
    }

    #endregion

    #region helpers

    private static List<ProjectModel?> GetDomains()
    {
        return
            [
                new()
                {
                    Id = 1,
                    Name = "Project A"
                },
                null,
                new()
                {
                    Id = 2,
                    Name = "Project B"
                }
            ];
    }

    private static IEnumerable<ProjectModel?> GetViewModels()
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

    private static void AssertEqualDomainsAndViewModels(
        IEnumerable<ProjectModel?> domains,
        IEnumerable<ProjectModel?> viewModels)
    {
        var count = domains.Count();
        Assert.Equal(count, viewModels.Count());
        for (int i = 0; i < count; i++)
        {
            if (domains.ElementAt(i) == null)
            {
                Assert.Null(viewModels.ElementAt(i));
            }
            else
            {
                Assert.Equal(domains.ElementAt(i)?.Id, viewModels.ElementAt(i)?.Id);
                Assert.Equal(domains.ElementAt(i)?.Name, viewModels.ElementAt(i)?.Name);
            }
        }
    }

    private static void AssertNotEqualDomainsAndViewModels(
        IEnumerable<ProjectModel?> domains,
        IEnumerable<ProjectModel?> viewModels)
    {
        var count = Math.Min(domains.Count(), viewModels.Count());
        for (int i = 0; i < count; i++)
        {
            Assert.NotEqual(domains.ElementAt(i)?.Id, viewModels.ElementAt(i)?.Id);
            Assert.NotEqual(domains.ElementAt(i)?.Name, viewModels.ElementAt(i)?.Name);
        }
    }

    #endregion
}
