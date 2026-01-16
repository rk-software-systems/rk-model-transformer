using RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionClassPrimitiveImplicit;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class CollectionClassPrimitiveImplicitTests
{
    #region List

    [Fact]
    public void TestCreateViewModelToList()
    {
        var domain = new CompanyDomain
        {
            ProjectList = ["p1", null, "p2", "p3"]
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
            ProjectList = ["p1", null, "p2", "p3"]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectList = ["p4", "p5", "p6", null]
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
            ProjectList = ["p4", "p5", "p6", null]
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
            ProjectCollection = [1, null, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectCollection);

        AssertEqualDomainsAndViewModels(domain.ProjectCollection.Select(x => (long?)x), viewModel.ProjectCollection);
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
            ProjectCollection = [1, null, 2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectCollection = [4, 5, 6, null]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectCollection.Select(x => (long?)x), viewModel.ProjectCollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectCollection);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectCollection.Select(x => (long?)x), viewModel.ProjectCollection);
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
            ProjectCollection = [4, 5, 6, null]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectCollection);
    }

    #endregion      

    #region ReadOnlyCollection

    [Fact]
    public void TestCreateViewModelToReadOnlyCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectReadOnlyCollection = [1, null, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectReadOnlyCollection);

        AssertEqualDomainsAndViewModels(domain.ProjectReadOnlyCollection.Select(x => (decimal?)x), viewModel.ProjectReadOnlyCollection);
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
            ProjectReadOnlyCollection = [1, null, 2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectReadOnlyCollection = [4, 5, 6, null]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectReadOnlyCollection.Select(x => (decimal?)x), viewModel.ProjectReadOnlyCollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectReadOnlyCollection);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels(domain.ProjectReadOnlyCollection.Select(x => (decimal?)x), viewModel.ProjectReadOnlyCollection);
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
            ProjectReadOnlyCollection = [4, 5, 6, null]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectReadOnlyCollection);
    }

    #endregion

    #region helpers

    
    private static void AssertEqualDomainsAndViewModels<T>(
        IEnumerable<T> domains,
        IEnumerable<T> viewModels)
    {
        var count = domains.Count();
        Assert.Equal(count, viewModels.Count());
        for (int i = 0; i < count; i++)
        {
            Assert.Equal(domains.ElementAt(i), viewModels.ElementAt(i));
        }
    }

    private static void AssertNotEqualDomainsAndViewModels<T>(
        IEnumerable<T> domains,
        IEnumerable<T> viewModels)
    {
        var count = Math.Min(domains.Count(), viewModels.Count());        
        for (int i = 0; i < count; i++)
        {
            Assert.NotEqual(domains.ElementAt(i), viewModels.ElementAt(i));
        }
    }

    #endregion
}
