using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitiveImplicit;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ArrayPrimitiveImplicitTests
{
    #region Strings

    [Fact]
    public void TestCreateViewModelToArrayOfStrings()
    {
        var domain = new CompanyDomain
        {
            ProjectNames = ["p12", null, "p34", "p56"]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectNames);
        Assert.False(domain.ProjectNames == viewModel.ProjectNames);

        AssertEqualDomainsAndViewModels(domain.ProjectNames, viewModel.ProjectNames);
    }

    [Fact]
    public void TestCreateViewModelToArrayOfStringsFromNull()
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
    public void TestUpdateViewModelToArrayOfStrings()
    {
        var domain = new CompanyDomain
        {
            ProjectNames = ["p12", null, "p34", "p56"]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectNames = ["p46", "p78", "p11", null]
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
    public void TestUpdateViewModelToArrayOfStringsFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectNames = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectNames = ["p46", null, "p78", "p11"]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectNames);
    }

    #endregion

    #region Integers

    [Fact]
    public void TestCreateViewModelToArrayOfIntegers()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = [1, null, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIds);
        Assert.False(object.ReferenceEquals(domain.ProjectIds, viewModel.ProjectIds));

        AssertEqualDomainsAndViewModels<long?>(domain.ProjectIds.Select(x => (long?)x), viewModel.ProjectIds);
    }

    [Fact]
    public void TestCreateViewModelToArrayOfIntegersFromNull()
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
    public void TestUpdateViewModelToArrayOfIntegers()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = [1, null, 2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIds = [4, 5, 6, null]
        };

        AssertNotEqualDomainsAndViewModels(domain.ProjectIds.Select(x => (long?)x), viewModel.ProjectIds);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIds);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(object.ReferenceEquals(domain.ProjectIds, viewModel.ProjectIds));

        AssertEqualDomainsAndViewModels(domain.ProjectIds.Select(x => (long?)x), viewModel.ProjectIds);
    }

    [Fact]
    public void TestUpdateViewModelToArrayOfIntegersFromNull()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = null
        };

        var viewModel = new CompanyViewModel
        {
            ProjectIds = [4, 5, 6, null]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.ProjectIds);
    }

    #endregion

    #region Double -> Decimal

    [Fact]
    public void TestCreateViewModelFromArrayOfDoublesToArrayOfDecimals()
    {
        var domain = new CompanyDomain
        {
            Prices = [1, null, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Prices);
        Assert.False(object.ReferenceEquals(domain.Prices, viewModel.Prices));

        AssertEqualDomainsAndViewModels<decimal?>(domain.Prices.Select(x => (decimal?)x), viewModel.Prices);
    }

    [Fact]
    public void TestCreateViewModelArrayOfDoublesToArrayOfDecimalsWithNull()
    {
        var domain = new CompanyDomain
        {
            Prices = null
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);

        Assert.Null(viewModel.Prices);
    }

    [Fact]
    public void TestUpdateViewModelArrayOfDoublesToArrayOfDecimals()
    {
        var domain = new CompanyDomain
        {
            Prices = [1, null, 2, 3]
        };

        var viewModel = new CompanyViewModel
        {
            Prices = [4, 5, 6, null]
        };

        AssertNotEqualDomainsAndViewModels(domain.Prices.Select(x => (decimal?)x), viewModel.Prices);

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.Prices);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.False(object.ReferenceEquals(domain.Prices, viewModel.Prices));

        AssertEqualDomainsAndViewModels(domain.Prices.Select(x => (decimal?)x), viewModel.Prices);
    }

    [Fact]
    public void TestUpdateViewModelArrayOfDoublesToArrayOfDecimalsWithNull()
    {
        var domain = new CompanyDomain
        {
            Prices = null
        };

        var viewModel = new CompanyViewModel
        {
            Prices = [4, 5, 6, null]
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);
        Assert.Null(viewModel.Prices);
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
