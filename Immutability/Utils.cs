//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class Utils {
		public static bool TypeIsImmutable(TypeNode type) {
			if (type.IsPrimitive) return true;
			if (type is EnumNode) return true;
			if (type is Struct) return true;
			if (type is ArrayType) return false;
			if (type.FullName == "System.Collections.IEnumerable") return true;
			if (type.IsGeneric && type.FullName.StartsWith("System.Collections.Generic.IEnumerable`1")) return true;
			if (type.IsGeneric && type.FullName.StartsWith("System.Collections.Generic.IDictionary`2")) return true;
			return TypeHasImmutableAttribute(type);
		}

		public static bool TypeHasImmutableAttribute(TypeNode type) {
			foreach (var attribute in type.Attributes) {
				if (attribute.Type.FullName == "Jgr.ImmutableAttribute") {
					return true;
				}
			}
			return false;
		}

		public static void EnsureSourceInformation(Method method) {
			// Just calling the Body property getter is enough. Sigh.
			var x = method.Body;
		}
	}
}
