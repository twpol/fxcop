//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
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
						foreach (var caller in CallGraph.CallersFor(method))
						{
							var callThreadName = Utils.GetThreadName(caller);
							var callOnThreads = Utils.GetCallOnThreadList(Utils.GetBaseMember(caller));
							if ((callThreadName.Length == 0) && (callOnThreads.Length == 0))
							{
								this.Problems.Add(new Problem(GetResolution(member.FullName, String.Join(", ", threads), caller.FullName), member, member.FullName + ":CalledOnUnknownThread"));
							}
						}
					}
				}
			}
			return base.Problems;
		}
	}
}
