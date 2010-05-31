//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class ImmutableDescendants : BaseIntrospectionRule {
		public ImmutableDescendants()
			: base("ImmutableDescendants", "Immutability.FxCop", typeof(ImmutableDescendants).Assembly) {
		}

		public override ProblemCollection Check(TypeNode type) {
			if ((type.BaseType != null) && Utils.TypeHasImmutableAttribute(type.BaseType) && !Utils.TypeHasImmutableAttribute(type)) {
				base.Problems.Add(new Problem(GetResolution(type.FullName, type.BaseType.FullName)));
			}
			return base.Problems;
		}
	}
}
