//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using Microsoft.FxCop.Sdk;

namespace Immutability {
	class Utils {
		public static bool TypeIsImmutable(TypeNode type) {
			if (type is ArrayType) return false;
			if (type.FullName == "System.Byte") return true;
			if (type.FullName == "System.Sbyte") return true;
			if (type.FullName == "System.Int16") return true;
			if (type.FullName == "System.UInt16") return true;
			if (type.FullName == "System.Int32") return true;
			if (type.FullName == "System.UInt32") return true;
			if (type.FullName == "System.Int64") return true;
			if (type.FullName == "System.UInt64") return true;
			if (type.FullName == "System.String") return true;
			foreach (var attribute in type.Attributes) {
				if (attribute.Type.FullName == "Jgr.ImmutableAttribute") {
					return true;
				}
			}
			return false;
		}

	}
}
