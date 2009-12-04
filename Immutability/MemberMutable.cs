//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
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
				if (field != null) {
					if (!field.IsStatic) {
						if (!Utils.TypeIsImmutable(field.Type)) {
							base.Problems.Add(new Problem(GetResolution(member.FullName, field.Type.FullName)));
						}
					}
				} else if (property != null) {
					if (!property.IsStatic) {
						if (!Utils.TypeIsImmutable(property.Type)) {
							base.Problems.Add(new Problem(GetResolution(member.FullName, property.Type.FullName)));
						}
					}
				}
			}
			return base.Problems;
		}
	}
}
