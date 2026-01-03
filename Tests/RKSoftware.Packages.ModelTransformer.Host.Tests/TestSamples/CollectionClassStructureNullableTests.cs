using RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassStructureNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class CollectionClassStructureNullableTests
{
    #region List

    [Fact]
    public void TestCreateViewModelToList()
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
    public void TestCreateViewModelToListFromNull()
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
    public void TestUpdateViewModelToList()
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
    public void TestUpdateViewModelToListFromNull()
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

    #endregion

    #region Collection

    [Fact]
    public void TestCreateViewModelToCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectCollection = [..GetDomains()]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectCollection);

        AssertEqualDomainsAndViewModels(domain.ProjectCollection, viewModel.ProjectCollection);
    }

    [Fact]
    public void TestCreateViewModelToCollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectCollection = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectCollection);
    }

    [Fact]
    public void TestUpdateViewModelToCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectCollection = [..GetDomains()]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectCollection = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectCollection, viewModel.ProjectCollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectCollection);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectCollection, viewModel.ProjectCollection);
    }

    [Fact]
    public void TestUpdateViewModelToCollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectCollection = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectCollection = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectCollection);
    }

    #endregion

    #region LinkedList

    [Fact]
    public void TestCreateViewModelToLinkedList()
    {
        var domain = new CompanyDomain
        {
            ProjectLinkedList = new LinkedList<ProjectModel?>(GetDomains())
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectLinkedList);

        AssertEqualDomainsAndViewModels(domain.ProjectLinkedList, viewModel.ProjectLinkedList);
    }

    [Fact]
    public void TestCreateViewModelToLinkedListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectLinkedList = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectLinkedList);
    }

    [Fact]
    public void TestUpdateViewModelToLinkedList()
    {
        var domain = new CompanyDomain
        {
            ProjectLinkedList = new LinkedList<ProjectModel?>(GetDomains())
        };

        var viewModel = new CompanyViewModel
        {
            ProjectLinkedList = new LinkedList<ProjectModel?>(GetViewModels())
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectLinkedList, viewModel.ProjectLinkedList);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectLinkedList);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectLinkedList, viewModel.ProjectLinkedList);
    }

    [Fact]
    public void TestUpdateViewModelToLinkedListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectLinkedList = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectLinkedList = new LinkedList<ProjectModel?>(GetViewModels())
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectLinkedList);
    }

    #endregion

    #region ReadOnlyCollection

    [Fact]
    public void TestCreateViewModelToReadOnlyCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectReadOnlyCollection = [..GetDomains()]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectReadOnlyCollection);

        AssertEqualDomainsAndViewModels(domain.ProjectReadOnlyCollection, viewModel.ProjectReadOnlyCollection);
    }

    [Fact]
    public void TestCreateViewModelToReadOnlyCollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectReadOnlyCollection = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectReadOnlyCollection);
    }

    [Fact]
    public void TestUpdateViewModelToReadOnlyCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectReadOnlyCollection = [..GetDomains()]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectReadOnlyCollection = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectReadOnlyCollection, viewModel.ProjectReadOnlyCollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectReadOnlyCollection);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectReadOnlyCollection, viewModel.ProjectReadOnlyCollection);
    }

    [Fact]
    public void TestUpdateViewModelToReadOnlyCollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectReadOnlyCollection = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectReadOnlyCollection = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectReadOnlyCollection);
    }

    #endregion

    #region Queue

    [Fact]
    public void TestCreateViewModelToQueue()
    {
        var domain = new CompanyDomain
        {
            ProjectQueue = new Queue<ProjectModel?>(GetDomains())
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectQueue);

        AssertEqualDomainsAndViewModels(domain.ProjectQueue, viewModel.ProjectQueue);
    }

    [Fact]
    public void TestCreateViewModelToQueueFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectQueue = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectQueue);
    }

    [Fact]
    public void TestUpdateViewModelToQueue()
    {
        var domain = new CompanyDomain
        {
            ProjectQueue = new Queue<ProjectModel?>(GetDomains())
        };

        var viewModel = new CompanyViewModel
        {
            ProjectQueue = new Queue<ProjectModel?>(GetViewModels())
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectQueue, viewModel.ProjectQueue);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectQueue);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectQueue, viewModel.ProjectQueue);
    }

    [Fact]
    public void TestUpdateViewModelToQueueFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectQueue = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectQueue = new Queue<ProjectModel?>(GetViewModels())
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectQueue);
    }

    #endregion

    #region Stack

    [Fact]
    public void TestCreateViewModelToStack()
    {
        var domain = new CompanyDomain
        {
            ProjectStack = new Stack<ProjectModel?>(GetDomains())
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectStack);

        AssertEqualDomainsAndViewModels(domain.ProjectStack, viewModel.ProjectStack.Reverse());
    }

    [Fact]
    public void TestCreateViewModelToStackFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectStack = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectStack);
    }

    [Fact]
    public void TestUpdateViewModelToStack()
    {
        var domain = new CompanyDomain
        {
            ProjectStack = new Stack<ProjectModel?>(GetDomains())
        };

        var viewModel = new CompanyViewModel
        {
            ProjectStack = new Stack<ProjectModel?>(GetViewModels())
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectStack, viewModel.ProjectStack);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectStack);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectStack, viewModel.ProjectStack.Reverse());
    }

    [Fact]
    public void TestUpdateViewModelToStackFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectStack = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectStack = new Stack<ProjectModel?>(GetViewModels())
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectStack);
    }

    #endregion

    #region HashSet

    [Fact]
    public void TestCreateViewModelToHashSet()
    {
        var domain = new CompanyDomain
        {
            ProjectHashSet = [.. GetDomains()]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectHashSet);

        AssertEqualDomainsAndViewModels(domain.ProjectHashSet, viewModel.ProjectHashSet);
    }

    [Fact]
    public void TestCreateViewModelToHashSetFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectHashSet = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectHashSet);
    }

    [Fact]
    public void TestUpdateViewModelToHashSet()
    {
        var domain = new CompanyDomain
        {
            ProjectHashSet = [.. GetDomains()]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectHashSet = [.. GetViewModels()]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectHashSet, viewModel.ProjectHashSet);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectHashSet);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectHashSet, viewModel.ProjectHashSet);
    }

    [Fact]
    public void TestUpdateViewModelToHashSetFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectHashSet = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectHashSet = [.. GetViewModels()]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectHashSet);
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
            Assert.Equal(domains.ElementAt(i)?.Id, viewModels.ElementAt(i)?.Id);
            Assert.Equal(domains.ElementAt(i)?.Name, viewModels.ElementAt(i)?.Name);
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
