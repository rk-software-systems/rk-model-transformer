using RKSoftware.Packages.ModelTransformer.Host.TestSamples.Base;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests.TestSamples;

public class BaseTests
{
    private const int _id = 1;
    private const string _description = "A sample company";
    private const int _establishedYear = 1990;


    [Fact]
    public void TestCreateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Id = _id,
            Description = _description,
            EstablishedYear = _establishedYear
        };

        var viewModel = domain.Transform();

        Assert.NotNull(viewModel);
        Assert.Equal(domain.Id, viewModel.Id);
        Assert.Equal(_description, viewModel.Description);
        Assert.Equal(_establishedYear, viewModel.EstablishedYear);
    }

    [Fact]
    public void TestUpdateViewModelFromDomain()
    {
        var domain = new CompanyDomain
        {
            Id = _id,
            Description = _description,
            EstablishedYear = _establishedYear
        };

        var viewModel = new CompanyViewModel
        {
            Id = 99,
            Description = "Old Description",
            EstablishedYear = 2000
        };

        var updatedViewModel = domain.Transform(viewModel);

        Assert.NotNull(updatedViewModel);
        Assert.Equal(viewModel, updatedViewModel);

        Assert.Equal(domain.Id, updatedViewModel.Id);
        Assert.Equal(_description, viewModel.Description);
        Assert.Equal(_establishedYear, viewModel.EstablishedYear);
    }

    [Fact]
    public void TestCreateViewModelListFromDomainList()
    {
        var domains = new List<CompanyDomain>
        {
            new ()
            {
                Id = _id,
                Description = _description,
                EstablishedYear = _establishedYear
            },
            new ()
            {
                Id = 2,
                Description = "Another Company",
                EstablishedYear = 2000
            }
        };

        var viewModels = domains.Transform();

        Assert.NotNull(viewModels);
        Assert.Equal(domains.Count, viewModels.Count);

    }

    [Fact]
    public void TestCreateViewModelEnumerableFromDomainEnumerable()
    {
        var domains = new List<CompanyDomain>
        {
            new ()
            {
                Id = _id,
                Description = _description,
                EstablishedYear = _establishedYear
            },
            new ()
            {
                Id = 2,
                Description = "Another Company",
                EstablishedYear = 2000
            }
        };

        var viewModels = domains.Select(x => x).Transform();
        Assert.NotNull(viewModels);
        Assert.Equal(domains.Count, viewModels.Count());
    }

    [Fact]
    public void TestCreateViewModelCollectionFromDomainCollection()
    {
        ICollection<CompanyDomain> domains =
        [
            new ()
            {
                Id = _id,
                Description = _description,
                EstablishedYear = _establishedYear
            },
            new ()
            {
                Id = 2,
                Description = "Another Company",
                EstablishedYear = 2000
            }
        ];

        var viewModels = domains.Transform();
        Assert.NotNull(viewModels);
        Assert.Equal(domains.Count, viewModels.Count);
    }
}
