//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class MemberMutable : BaseIntrospectionRule {
		public MemberMutable()
			: base("MemberMutable", "Immutability.FxCop", typeof(MemberMutable).Assembly) {
		}

		public override ProblemCollection Check(Member member) {
			if (Utils.TypeHasImmutableAttribute(member.DeclaringType)) {
				var field = member as Field;
				var property = member as PropertyNode;
				if ((field != null) && !field.IsStatic && !Utils.TypeIsImmutable(field.Type)) {
					base.Problems.Add(new Problem(GetResolution(member.FullName, field.Type.FullName)));
				} else if ((property != null) && !property.IsStatic && !Utils.TypeIsImmutable(property.Type)) {
					base.Problems.Add(new Problem(GetResolution(member.FullName, property.Type.FullName)));
				}
			}
			return base.Problems;
		}
	}
}
