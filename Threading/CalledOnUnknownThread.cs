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
	public class CalledOnUnknownThread : BaseIntrospectionRule
	{
		public CalledOnUnknownThread()
			: base("CalledOnUnknownThread", "Threading.FxCop", typeof(CalledOnUnknownThread).Assembly)
		{
		}

		public override ProblemCollection Check(Member member)
		{
			if (!member.OverridesBaseClassMember)
			{
				var method = member as Method;
                if ((method != null) && !member.IsStatic)
                {
                    // Is this member one we care about?
                    var threads = Utils.GetCallOnThreadList(member);
                    if (threads.Length > 0)
                    {
                        // Find out what threads we're called from.
                        var callingThreads = Utils.GetCallingThreads(method);
                        if (callingThreads.Length == 0)
                        {
                            Problems.Add(new Problem(GetResolution(member.FullName), member, member.FullName + ":CalledOnUnknownThread:2"));
                        }
                    }
                }
			}
			return Problems;
		}
	}
}
