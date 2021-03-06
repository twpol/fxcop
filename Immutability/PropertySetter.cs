﻿//------------------------------------------------------------------------------
// http://james-ross.co.uk/projects/fxcop
// License: New BSD License (BSD).
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
				if ((property != null) && !property.IsStatic && (property.Setter != null) && property.Setter.IsVisibleOutsideAssembly) {
					base.Problems.Add(new Problem(GetResolution(member.FullName), property.Setter));
				}
			}
			return base.Problems;
		}
	}
}
