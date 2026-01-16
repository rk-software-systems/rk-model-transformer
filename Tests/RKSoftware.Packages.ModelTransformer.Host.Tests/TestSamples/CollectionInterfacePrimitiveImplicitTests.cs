using RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveImplicit;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class CollectionInterfacePrimitiveImplicitTests
{
    #region IList

    [Fact]
    public void TestCreateViewModelToIList()
    {
        var domain = new CompanyDomain
        {
            ProjectIList = ["12", null, "34", "56"]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIList);
        Assert.False(domain.ProjectIList == viewModel.ProjectIList);

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
            ProjectIList = ["12", null, "34", "56"]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIList = ["46", "78", "11", null]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIList, viewModel.ProjectIList);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIList);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectIList == viewModel.ProjectIList);

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
            ProjectIList = ["46", null, "78", "11"]
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
            ProjectICollection = [23, null, 456, 98]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectICollection);
        Assert.False(domain.ProjectICollection == viewModel.ProjectICollection);

        AssertEqualDomainsAndViewModels(domain.ProjectICollection.Select(x => (decimal?)x), viewModel.ProjectICollection);
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
            ProjectICollection = [23, null, 456, 98]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectICollection = [1, 35, 123, null]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectICollection.Select(x => (decimal?)x), viewModel.ProjectICollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectICollection);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectICollection == viewModel.ProjectICollection);

        AssertEqualDomainsAndViewModels(domain.ProjectICollection.Select(x => (decimal?)x), viewModel.ProjectICollection);
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
            ProjectICollection = [1, 35, 123, null]
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
            ProjectIEnumerable = [1, null, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIEnumerable);
        Assert.False(domain.ProjectIEnumerable == viewModel.ProjectIEnumerable);

        AssertEqualDomainsAndViewModels(domain.ProjectIEnumerable.Select(x => (long?)x), viewModel.ProjectIEnumerable);
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
            ProjectIEnumerable = [1, null, 2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIEnumerable = [4, 5, 6, null]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIEnumerable.Select(x => (long?)x), viewModel.ProjectIEnumerable);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIEnumerable);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectIEnumerable == viewModel.ProjectIEnumerable);

        AssertEqualDomainsAndViewModels(domain.ProjectIEnumerable.Select(x => (long?)x), viewModel.ProjectIEnumerable);
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
            ProjectIEnumerable = [4, null, 5, 6]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIEnumerable);
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
