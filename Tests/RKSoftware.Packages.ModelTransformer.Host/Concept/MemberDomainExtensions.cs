namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public static partial class MemberDomainExtensions
{
    #region to ViewModel

    public static MemberViewModel ToViewModel(this MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var memberId = ToViewModelMemberIdDefault(source);
        ToViewModelMemberId(source, ref memberId);

        var userName = ToViewModelUserNameDefault(source);
        ToViewModelUserName(source, ref userName);

        var target = new MemberViewModel
        {
            MemberId = memberId,
            UserName = userName
        };

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

    #endregion

    #region to Dto

    public static MemberDto ToDto(this MemberDomain source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var memberId = ToDtoMemberIdDefault(source);
        ToDtoMemberId(source, ref memberId);

        var userName = ToDtoUserNameDefault(source);
        ToDtoUserName(source, ref userName);

        var target = new MemberDto
        {
            MemberId = memberId,
            UserName = userName
        };

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
