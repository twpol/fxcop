//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class PropertySetter : BaseIntrospectionRule {
		public PropertySetter()
			: base("PropertySetter", "Immutability.FxCop", typeof(FieldReadonly).Assembly) {
		}

		public override TargetVisibilities TargetVisibility {
			get {
				return TargetVisibilities.All;
			}
		}

		public override ProblemCollection Check(Member member) {
			if (Utils.TypeIsImmutable(member.DeclaringType)) {
				var property = member as PropertyNode;
				if (property != null) {
					if (!property.IsStatic) {
						if (property.Setter != null) {
							if ((property.Setter.Flags & MethodFlags.Private) == 0) {
								this.Problems.Add(new Problem(GetResolution(member.FullName)));
							}
						}
					}
				}
			}
			return this.Problems;
		}
	}
}
