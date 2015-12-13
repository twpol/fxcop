//------------------------------------------------------------------------------
// http://james-ross.co.uk/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace Threading
{
	public static class Utils
	{
		public static Member GetBaseMember(Member member)
		{
			while (member.OverridesBaseClassMember)
				member = member.OverriddenMember;
			return member;
		}

		public static string[] GetCallOnThreadList(Member member)
		{
			return GetCallOnThreadList(member, true);
		}

		public static string[] GetCallOnThreadList(Member member, bool useTypeFallback)
		{
			var threadAttributes = GetAttributesOfType(member, "CallOnThreadAttribute", useTypeFallback);
			return threadAttributes.Select(a => a.GetPositionalArgument(0).ToString()).ToArray();
		}

		public static string GetThreadName(Member member)
		{
			return GetThreadName(member, true);
		}

		public static string GetThreadName(Member member, bool useTypeFallback)
		{
			var threadAttributes = GetAttributesOfType(member, "ThreadNameAttribute", useTypeFallback);
			if (!threadAttributes.Any())
				return "";
			return threadAttributes.First().GetPositionalArgument(0).ToString();
		}

		static IList<AttributeNode> GetAttributesOfType(Member member, string name, bool useTypeFallback)
		{
			// Check the member first, falling back to the type.
			var list = member.Attributes.Where(a => a.Type.Name.Name == name);
			if (useTypeFallback && !list.Any())
				list = member.DeclaringType.Attributes.Where(a => a.Type.Name.Name == name);
			return list.ToList();
		}
	}
}
