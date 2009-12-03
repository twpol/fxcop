//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class MemberMutable : BaseIntrospectionRule {
		public MemberMutable()
			: base("MemberMutable", "Immutability.FxCop", typeof(FieldReadonly).Assembly) {
		}

		public override TargetVisibilities TargetVisibility {
			get {
				return TargetVisibilities.All;
			}
		}

		public override ProblemCollection Check(Member member) {
			if (Utils.TypeIsImmutable(member.DeclaringType)) {
				var field = member as Field;
				var property = member as PropertyNode;
				if (field != null) {
					if (!field.IsStatic) {
						if (!Utils.TypeIsImmutable(field.Type)) {
							this.Problems.Add(new Problem(GetResolution(member.FullName, field.Type.FullName)));
						}
					}
				} else if (property != null) {
					if (!property.IsStatic) {
						if (!Utils.TypeIsImmutable(property.Type)) {
							this.Problems.Add(new Problem(GetResolution(member.FullName, property.Type.FullName)));
						}
					}
				}
			}
			return this.Problems;
		}
	}
}
