using System.Collections.ObjectModel;

namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public class MemberViewModel
{
    public long MemberId { get; set; }

    public required string UserName { get; set; }

    public AddressViewModel? Address { get; set; }

    public Collection<AddressViewModel?>? Addresses { get; set; }

    public required string FirstName { get; set; }

    public LinkedList<DepartmentViewModel>? Departments { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
    public int[]? Scores { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    public IList<DateTime>? UpdatedDates { get; set; }

    public List<int?>? ProfileIds { get; set; }
}
