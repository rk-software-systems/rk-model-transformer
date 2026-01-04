

namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

[MemberRegistration<MemberDomain, MemberDto>(Ignored = [nameof(MemberDto.MemberId)])]
public class MemberRegistration
{
}
