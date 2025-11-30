namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public class MemberViewModel
{
    public long MemberId { get; set; }

    public required string UserName { get; set; }

    public AddressViewModel? Address { get; set; }
}
