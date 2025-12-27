namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public class MemberDomain
{
    public long MemberId { get; set; }

    public required string UserName { get; set; }

    public AddressDomain? Address { get; set; }

    public List<AddressDomain>? Addresses { get; set; }

    public string? FirstName { get; set; }

    public LinkedList<DepartmentDomain>? Departments { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
    public int[]? Scores { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
}
