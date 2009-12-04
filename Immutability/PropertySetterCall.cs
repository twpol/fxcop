//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class PropertySetterCall : BaseIntrospectionRule {
		public PropertySetterCall()
			: base("PropertySetterCall", "Immutability.FxCop", typeof(PropertySetterCall).Assembly) {
		}

		public override ProblemCollection Check(Member member) {
			if (Utils.TypeHasImmutableAttribute(member.DeclaringType)) {
				var property = member as PropertyNode;
				if (property != null) {
					if (!property.IsStatic) {
						if (property.Setter != null) {
							foreach (var caller in CallGraph.CallersFor(property.Setter)) {
								if (!(caller is InstanceInitializer)) {
									Utils.EnsureSourceInformation(caller);
									base.Problems.Add(new Problem(GetResolution(member.FullName, caller.FullName), caller, caller.FullName + "->" + member.FullName));
								}
							}
						}
					}
				}
			}
			return base.Problems;
		}
	}
}
