using RKSoftware.Packages.ModelTransformer.Host.TestSamples.CollectionInterfacePrimitiveNotNullableToNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class CollectionInterfacePrimitiveNotNullableToNullableTests
{
    #region IList

    [Fact]
    public void TestCreateViewModelToIList()
    {
        var domain = new CompanyDomain
        {
            ProjectIList = ["12", "34", "56"]
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
            ProjectIList = ["12", "34", "56"]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIList = ["46", null, "78", "11"]
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
            ProjectICollection = [new DateTime(2020, 1, 1), new DateTime(2021, 2, 2), new DateTime(2022, 3, 3)]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectICollection);
        Assert.False(domain.ProjectICollection == viewModel.ProjectICollection);

        AssertEqualDomainsAndViewModels<DateTime?>(domain.ProjectICollection.Cast<DateTime?>(), viewModel.ProjectICollection);
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
            ProjectICollection = [new DateTime(2020, 1, 1), new DateTime(2021, 2, 2), new DateTime(2022, 3, 3)]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectICollection = [new DateTime(2010, 1, 1), null, new DateTime(2011, 2, 2), new DateTime(2012, 3, 3)]
        };

        AssertNotEqualDomainsAndViewModels<DateTime?>(domain.ProjectICollection.Cast<DateTime?>(), viewModel.ProjectICollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectICollection);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectICollection == viewModel.ProjectICollection);

        AssertEqualDomainsAndViewModels<DateTime?>(domain.ProjectICollection.Cast<DateTime?>(), viewModel.ProjectICollection);
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
            ProjectICollection = [new DateTime(2010, 1, 1), null, new DateTime(2011, 2, 2), new DateTime(2012, 3, 3)]
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
            ProjectIEnumerable = [1, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIEnumerable);
        Assert.False(domain.ProjectIEnumerable == viewModel.ProjectIEnumerable);

        AssertEqualDomainsAndViewModels<int?>(domain.ProjectIEnumerable.Cast<int?>(), viewModel.ProjectIEnumerable);
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
            ProjectIEnumerable = [1,  2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIEnumerable = [4, null, 5, 6]
        };

        AssertNotEqualDomainsAndViewModels<int?>(domain.ProjectIEnumerable.Cast<int?>(), viewModel.ProjectIEnumerable);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIEnumerable);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectIEnumerable == viewModel.ProjectIEnumerable);

        AssertEqualDomainsAndViewModels<int?>(domain.ProjectIEnumerable.Cast<int?>(), viewModel.ProjectIEnumerable);
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

    #region IReadOnlyCollection

    [Fact]
    public void TestCreateViewModelToIReadOnlyCollection()
    {
        var domain = new CompanyDomain
        {
            ProjectIReadOnlyCollection = [1, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIReadOnlyCollection);
        Assert.False(domain.ProjectIReadOnlyCollection == viewModel.ProjectIReadOnlyCollection);

        AssertEqualDomainsAndViewModels<long?>(domain.ProjectIReadOnlyCollection.Cast<long?>(), viewModel.ProjectIReadOnlyCollection);
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
            ProjectIReadOnlyCollection = [1,  2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIReadOnlyCollection = [4, null, 5, 6]
        };

        AssertNotEqualDomainsAndViewModels<long?>(domain.ProjectIReadOnlyCollection.Cast<long?>(), viewModel.ProjectIReadOnlyCollection);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIReadOnlyCollection);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectIReadOnlyCollection == viewModel.ProjectIReadOnlyCollection);

        AssertEqualDomainsAndViewModels<long?>(domain.ProjectIReadOnlyCollection.Cast<long?>(), viewModel.ProjectIReadOnlyCollection);
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
            ProjectIReadOnlyCollection = [4, 5, 6]
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
            ProjectIReadOnlyList = [1, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIReadOnlyList);
        Assert.False(domain.ProjectIReadOnlyList == viewModel.ProjectIReadOnlyList);

        AssertEqualDomainsAndViewModels<byte?>(domain.ProjectIReadOnlyList.Cast<byte?>(), viewModel.ProjectIReadOnlyList);
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
            ProjectIReadOnlyList = [1, 2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIReadOnlyList = [4, null, 5, 6]
        };

        AssertNotEqualDomainsAndViewModels<byte?>(domain.ProjectIReadOnlyList.Cast<byte?>(), viewModel.ProjectIReadOnlyList);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIReadOnlyList);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectIReadOnlyList == viewModel.ProjectIReadOnlyList);

        AssertEqualDomainsAndViewModels<byte?>(domain.ProjectIReadOnlyList.Cast<byte?>(), viewModel.ProjectIReadOnlyList);
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
            ProjectIReadOnlyList = [4, 5, 6]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIReadOnlyList);
    }

    #endregion

    #region helpers

    private static void AssertEqualDomainsAndViewModels<T>(
        IEnumerable<T> domains,
        IEnumerable<T> viewModels)
    {
        domains = domains.Where(x => x != null);
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
