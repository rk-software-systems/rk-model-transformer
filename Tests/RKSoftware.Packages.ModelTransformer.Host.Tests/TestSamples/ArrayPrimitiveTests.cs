using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitive;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ArrayPrimitiveTests
{
    #region Strings

    [Fact]
    public void TestCreateViewModelToIList()
    {
        var domain = new CompanyDomain
        {
            ProjectNames = ["p12", "p34", "p56"]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectNames);
        Assert.False(domain.ProjectNames == viewModel.ProjectNames);

        AssertEqualDomainsAndViewModels(domain.ProjectNames, viewModel.ProjectNames);
    }

    [Fact]
    public void TestCreateViewModelToIListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectNames = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectNames);
    }

    [Fact]
    public void TestUpdateViewModelToIList()
    {
        var domain = new CompanyDomain
        {
            ProjectNames = ["p12", "p34", "p56"]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectNames = ["p46", "p78", "p11"]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectNames, viewModel.ProjectNames);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectNames);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectNames == viewModel.ProjectNames);

        AssertEqualDomainsAndViewModels(domain.ProjectNames, viewModel.ProjectNames);
    }

    [Fact]
    public void TestUpdateViewModelToIListFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectNames = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectNames = ["p46", "p78", "p11"]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectNames);
    }

    #endregion

    #region Integers

    [Fact]
    public void TestCreateViewModelToICollection()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = [1, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIds);
        Assert.False(domain.ProjectIds == viewModel.ProjectIds);

        AssertEqualDomainsAndViewModels(domain.ProjectIds, viewModel.ProjectIds);
    }

    [Fact]
    public void TestCreateViewModelToICollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.ProjectIds);
    }

    [Fact]
    public void TestUpdateViewModelToICollection()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = [1, 2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIds = [4, 5, 6]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIds, viewModel.ProjectIds);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIds);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(domain.ProjectIds == viewModel.ProjectIds);

        AssertEqualDomainsAndViewModels(domain.ProjectIds, viewModel.ProjectIds);
    }

    [Fact]
    public void TestUpdateViewModelToICollectionFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIds = [4, 5, 6]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIds);
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
