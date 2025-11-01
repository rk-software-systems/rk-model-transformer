namespace RKSoftware.Packages.ModelTransformer.Host.Tests;

public class TransformerTests
{
    [Fact]
    public void UserToUserViewModelTransformerTest()
    {
        var domain = new User
        {
            StringOptional = "John Doe",
            StringRequired = "johndoe"
        };

        var viewModel = domain.Transform();

        Assert.Equal(domain.StringOptional, viewModel.StringOptional);
        Assert.Equal(domain.StringRequired, viewModel.StringRequired);
    }
}
