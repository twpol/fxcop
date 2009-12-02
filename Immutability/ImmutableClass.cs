using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace Immutability {
	public class ImmutableClass : BaseIntrospectionRule {
		public ImmutableClass()
			: base("ImmutableClass", "Immutability.FxCop", typeof(ImmutableClass).Assembly) {
		}

		public override TargetVisibilities TargetVisibility {
			get {
				return TargetVisibilities.All;
			}
		}
	}
}
