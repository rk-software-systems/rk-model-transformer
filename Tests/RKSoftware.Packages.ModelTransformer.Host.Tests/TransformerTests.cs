namespace RKSoftware.Packages.ModelTransformer.Host.Tests;

public class TransformerTests
{
    [Fact]
    public void UserToUserViewModelTransformerTest()
    {
        var domain = new User
        {
            Name = "John Doe",
            Username = "johndoe"
        };

        var viewModel = domain.Transform();

        Assert.Equal(domain.Name, viewModel.Name);
        Assert.Equal(domain.Username, viewModel.Username);
    }
}
