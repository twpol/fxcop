//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
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
