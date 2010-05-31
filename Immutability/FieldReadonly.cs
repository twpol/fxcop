//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	public class FieldReadonly : BaseIntrospectionRule {
		public FieldReadonly()
			: base("FieldReadonly", "Immutability.FxCop", typeof(FieldReadonly).Assembly) {
		}

		public override ProblemCollection Check(Member member) {
			if (Utils.TypeHasImmutableAttribute(member.DeclaringType)) {
				var field = member as Field;
				if ((field != null) && !field.IsStatic && !field.IsInitOnly) {
					base.Problems.Add(new Problem(GetResolution(member.FullName), field));
				}
			}
			return base.Problems;
		}
	}
}
