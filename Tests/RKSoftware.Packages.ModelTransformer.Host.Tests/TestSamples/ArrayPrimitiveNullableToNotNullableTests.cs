using RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayPrimitiveNullableToNotNullable;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class ArrayPrimitiveNullableToNotNullableTests
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
    public void TestUpdateViewModelToArrayOfStringsFromNull()
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
    public void TestCreateViewModelToArrayOfIntegers()
    {
        var domain = new CompanyDomain
        {
            ProjectIds = [1, null, 2, 3]
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.ProjectIds);

        AssertEqualDomainsAndViewModels<int?>(domain.ProjectIds, viewModel.ProjectIds.Cast<int?>());
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
            ProjectIds = [4, 5, 6]
        };

        AssertNotEqualDomainsAndViewModels<int?>(domain.ProjectIds, viewModel.ProjectIds.Cast<int?>());

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.NotNull(updatedViewModel.ProjectIds);
        Assert.Equal(viewModel, updatedViewModel);

        AssertEqualDomainsAndViewModels<int?>(domain.ProjectIds, viewModel.ProjectIds.Cast<int?>());
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
        domains = domains.Where(d => d != null);
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
