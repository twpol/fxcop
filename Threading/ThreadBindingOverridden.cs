//------------------------------------------------------------------------------
// http://james-ross.co.uk/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.FxCop.Sdk;

namespace Threading
{
	public class ThreadBindingOverridden : BaseIntrospectionRule
	{
		public ThreadBindingOverridden()
			: base("ThreadBindingOverridden", "Threading.FxCop", typeof(CalledOnUnknownThread).Assembly)
		{
		}

		public override ProblemCollection Check(Member member)
		{
			var method = member as Method;
			if ((method != null) && !member.IsStatic)
			{
				// Is this member one we care about?
				var threads = Utils.GetCallOnThreadList(member, false);

				// If the method is virtual, use the base version.
				while (member.OverridesBaseClassMember)
				{
					if ((threads.Length > 0) && (Utils.GetThreadName(member, false).Length == 0))
						Problems.Add(new Problem(GetResolution(member.FullName, string.Join(", ", threads), member.OverriddenMember.FullName), member, member.FullName + ":ThreadBindingOverridden"));

					member = member.OverriddenMember;
					method = member as Method;
					threads = Utils.GetCallOnThreadList(member, false);
				}
			}
			return Problems;
		}
	}
}
