//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	public class FieldReadonly : BaseIntrospectionRule {
		public FieldReadonly()
			: base("FieldReadonly", "Immutability.FxCop", typeof(FieldReadonly).Assembly) {
		}

		public override ProblemCollection Check(Member member) {
			if (Utils.TypeIsImmutable(member.DeclaringType)) {
				var field = member as Field;
				if (field != null) {
					if (!field.IsStatic) {
						if (!field.IsInitOnly) {
							this.Problems.Add(new Problem(GetResolution(member.FullName)));
						}
					}
				}
			}
			return this.Problems;
		}
	}
}
