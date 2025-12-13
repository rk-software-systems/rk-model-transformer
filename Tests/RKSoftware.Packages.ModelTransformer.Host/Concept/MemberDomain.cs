namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public class MemberDomain
{
    public long MemberId { get; set; }

    public required string UserName { get; set; }

    public AddressDomain? Address { get; set; }

    public List<AddressDomain>? Addresses { get; set; }

    public string? FirstName { get; set; }
}
