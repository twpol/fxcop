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

        static Dictionary<string, string[]> MemberThreads = new Dictionary<string, string[]>();

        public static string[] GetCallingThreads(Method method)
        {
            return GetCallingThreads(method, new HashSet<string>());
        }

        public static string[] GetCallingThreads(Method method, HashSet<string> visited)
        {
            if (!MemberThreads.ContainsKey(method.FullName))
            {
                // This should only happen if we've recursively ended up scanning this method.
                if (visited.Contains(method.FullName))
                    return new string[0];

                // In case there are any recursive call stacks.
                visited.Add(method.FullName);

                // Make a list of this method and each override version.
                var methods = new List<Method>();
                var currentMethod = method;
                while (currentMethod != null)
                {
                    methods.Add(currentMethod);
                    currentMethod = currentMethod.OverriddenMethod;
                }

                // Now get all the callers for all the methods and map them to threads (possibly recursively).
                var threads = methods
                    .SelectMany(m => CallGraph.CallersFor(m))
                    .SelectMany(caller =>
                    {
                        var callThreadName = GetThreadName(caller);
                        if (callThreadName.Length > 0)
                            return new[] { callThreadName };
                        return GetCallingThreads(caller, visited);
                    }).Distinct().ToArray();

                lock (MemberThreads)
                {
                    MemberThreads[method.FullName] = threads;
                }
            }

            return MemberThreads[method.FullName];
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
