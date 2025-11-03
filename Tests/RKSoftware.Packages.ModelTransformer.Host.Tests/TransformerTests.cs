using RKSoftware.Packages.ModelTransformer.Host.TestSuite;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests;

public class TransformerTests
{
    [Fact]
    public void DomainToViewModelTransformerTest()
    {
        var domain = new Domain
        {
            StringOptional = "John Doe",
            StringRequired = "johndoe",
            StringIgnored = "Ignore me"
        };

        var viewModel = domain.Transform();

        Assert.Equal(domain.StringOptional, viewModel.StringOptional);

        Assert.Equal(domain.StringRequired, viewModel.StringRequired);

        Assert.True(string.IsNullOrEmpty(viewModel.StringIgnored));

        Assert.False(string.IsNullOrEmpty(viewModel.StringMissed));
    }

    [Fact]
    public void DomainToDtoTransformerTest()
    {
        var domain = new Domain
        {
            StringOptional = "John Doe",
            StringRequired = "johndoe",
            StringIgnored = "Ignore me"
        };

        var dto = domain.ToDto();

        Assert.Equal(domain.StringOptional, dto.StringOptional);

        Assert.Equal(domain.StringRequired, dto.StringRequired);

        Assert.True(string.IsNullOrEmpty(dto.StringIgnored));

        Assert.False(string.IsNullOrEmpty(dto.StringMissed));
    }

    [Fact]
    public void DomainToRecordTransformerTest()
    {
        var domain = new Domain
        {
            StringOptional = "John Doe",
            StringRequired = "johndoe",
            StringIgnored = "Ignore me"
        };

        var record = domain.ToRecord();

        Assert.Equal(domain.StringOptional, record.StringOptional);

        Assert.Equal(domain.StringRequired, record.StringRequired);
    }
}
