

namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

[MemberRegistration<MemberDomain, MemberDto>(IgnoredProperties = [nameof(MemberDto.MemberId)])]
public class MemberRegistration
{
}
