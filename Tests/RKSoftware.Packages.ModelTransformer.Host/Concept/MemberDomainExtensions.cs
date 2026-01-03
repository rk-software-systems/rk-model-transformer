using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

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

        var scores = ToViewModelScoresDefault(source);
        ToViewModelScores(source, ref scores);

        var updatedDates = ToViewModelUpdatedDatesDefault(source);
        ToViewModelUpdatedDates(source, ref updatedDates);

        if (target == null)
        {
            target = new MemberViewModel
            {
                MemberId = memberId,
                UserName = userName,
                Address = address,
                Addresses = addresses,
                FirstName = firstName,
                Departments = departments,
                Scores = scores,
                UpdatedDates = updatedDates
            };
        } 
        else
        {
            target.MemberId = memberId;
            target.UserName = userName;
            target.Address = address;
            target.Addresses = addresses;
            target.Departments = departments;
            target.Scores = scores;
            target.UpdatedDates = updatedDates;
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

    private static Collection<AddressViewModel?>? ToViewModelAddressesDefault(MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return source.Addresses != null ? new(source.Addresses.Select(x => x?.Transform()).ToList()) : default;
    }
    static partial void ToViewModelAddresses(MemberDomain source, ref Collection<AddressViewModel?>? address);

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

    private static int[]? ToViewModelScoresDefault(MemberDomain source)
    {
        return source.Scores != null ? source.Scores[..] : default;
    }
    static partial void ToViewModelScores(MemberDomain source, ref int[]? target);

    private static IList<DateTime>? ToViewModelUpdatedDatesDefault(MemberDomain source)
    {
        return source.UpdatedDates != null ? [.. source.UpdatedDates] : default;
    }
    static partial void ToViewModelUpdatedDates(MemberDomain source, ref IList<DateTime>? target);
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
