namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public static partial class MemberDomainExtensions
{
    #region to ViewModel

    public static MemberViewModel Transform(this MemberDomain source, MemberViewModel? target = null)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var memberId = ToViewModelMemberIdDefault(source);
        ToViewModelMemberId(source, ref memberId);

        var userName = ToViewModelUserNameDefault(source);
        ToViewModelUserName(source, ref userName);

        var address = ToViewModelAddressDefault(source);
        ToViewModelAddress(source, ref address);

        var addresses = ToViewModelAddressesDefault(source);
        ToViewModelAddresses(source, ref addresses);

        var firstName = ToViewModelFirstNameDefault(source);
        ToViewModelFirstName(source, ref firstName);

        var departments = ToViewModelDepartmentsDefault(source);
        ToViewModelDepartments(source, ref departments);

        if (target == null)
        {
            target = new MemberViewModel
            {
                MemberId = memberId,
                UserName = userName,
                Address = address,
                Addresses = addresses,
                FirstName = firstName,
                Departments = departments
            };
        } 
        else
        {
            target.MemberId = memberId;
            target.UserName = userName;
            target.Address = address;
            target.Addresses = addresses;
            target.Departments = departments;
        }

        return target;
    }

    private static long ToViewModelMemberIdDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.MemberId;
    }
    static partial void ToViewModelMemberId(MemberDomain source, ref long memberId);

    private static string ToViewModelUserNameDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.UserName;
    }
    static partial void ToViewModelUserName(MemberDomain source, ref string userName);

    private static AddressViewModel? ToViewModelAddressDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.Address?.Transform();
    }
    static partial void ToViewModelAddress(MemberDomain source, ref AddressViewModel? address);

    private static List<AddressViewModel>? ToViewModelAddressesDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.Addresses != null ? [.. source.Addresses.Select(x => x.Transform())] : default;
    }
    static partial void ToViewModelAddresses(MemberDomain source, ref List<AddressViewModel>? address);

    private static string ToViewModelFirstNameDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.FirstName ?? string.Empty; // Example of custom default handling
    }
    static partial void ToViewModelFirstName(MemberDomain source, ref string firstName);


    private static LinkedList<DepartmentViewModel>? ToViewModelDepartmentsDefault(MemberDomain source)
    {
        return source.Departments != null ? new (source.Departments.Select(x => x.Transform())) : default;
    }
    static partial void ToViewModelDepartments(MemberDomain source, ref LinkedList<DepartmentViewModel>? target);
    #endregion

    #region to Dto

    public static MemberDto Transform(this MemberDomain source, MemberDto? target = null)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var memberId = ToDtoMemberIdDefault(source);
        ToDtoMemberId(source, ref memberId);

        var userName = ToDtoUserNameDefault(source);
        ToDtoUserName(source, ref userName);

        if (target == null)
        {
            target = new MemberDto
            {
                MemberId = memberId,
                UserName = userName
            };
        } 
        else
        {
            target.MemberId = memberId;
            target.UserName = userName;
        }


        return target;
    }

    private static long ToDtoMemberIdDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.MemberId;
    }
    static partial void ToDtoMemberId(MemberDomain source, ref long memberId);

    private static string ToDtoUserNameDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.UserName;
    }
    static partial void ToDtoUserName(MemberDomain source, ref string userName);

    #endregion
}
