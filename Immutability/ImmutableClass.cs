//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

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

		bool TypeIsImmutable(TypeNode type) {
			foreach (var attribute in type.Attributes) {
				if (attribute.Type.FullName == "Jgr.ImmutableAttribute") {
					return true;
				}
			}
			return false;
		}

		public override ProblemCollection Check(Member member) {
			if (TypeIsImmutable(member.DeclaringType)) {
				var field = member as Field;
				var property = member as PropertyNode;
				if (field != null) {
					if (!field.IsStatic) {
						if (!field.IsInitOnly) {
							this.Problems.Add(new Problem(GetNamedResolution("ReadonlyField", member.FullName)));
						}
						if (!TypeIsImmutable(field.Type)) {
							this.Problems.Add(new Problem(GetNamedResolution("MutableMemberType", member.FullName, field.Type.FullName)));
						}
					}
				} else if (property != null) {
					if (!property.IsStatic) {
						if (property.Setter != null) {
							if ((property.Setter.Flags & MethodFlags.Private) == 0) {
								this.Problems.Add(new Problem(GetNamedResolution("PropertySetter", member.FullName)));
							}
						}
						if (!TypeIsImmutable(property.Type)) {
							this.Problems.Add(new Problem(GetNamedResolution("MutableMemberType", member.FullName, property.Type.FullName)));
						}
					}
				}
			}
			return this.Problems;
		}
	}
}
