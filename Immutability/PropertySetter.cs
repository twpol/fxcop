//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class PropertySetter : BaseIntrospectionRule {
		public PropertySetter()
			: base("PropertySetter", "Immutability.FxCop", typeof(PropertySetter).Assembly) {
		}

		public override ProblemCollection Check(Member member) {
			if (Utils.TypeHasImmutableAttribute(member.DeclaringType)) {
				var property = member as PropertyNode;
				if (property != null) {
					if (!property.IsStatic) {
						if (property.Setter != null) {
							if ((property.Setter.Flags & MethodFlags.Private) == 0) {
								base.Problems.Add(new Problem(GetResolution(member.FullName), property.Setter));
							}
						}
					}
				}
			}
			return base.Problems;
		}
	}
}
