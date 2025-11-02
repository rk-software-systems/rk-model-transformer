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
}
