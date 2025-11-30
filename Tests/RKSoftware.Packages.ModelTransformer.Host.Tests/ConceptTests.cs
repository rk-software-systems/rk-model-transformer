using RKSoftware.Packages.ModelTransformer.Host.Concept;
using RKSoftware.Packages.ModelTransformer.Host.TestSuite;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests;

public class ConceptTests
{
    [Fact]
    public void DomainToViewModelTransformerTest()
    {
        var domain = new Concept.MemberDomain
        {
            MemberId = 1,
            UserName = "johndoe"
        };
        var viewModel = domain.Transform((MemberViewModel?)null);
        Assert.Equal(domain.MemberId, viewModel.MemberId);
        Assert.Equal(domain.UserName, viewModel.UserName);
    }
}
