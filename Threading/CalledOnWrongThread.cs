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
	public class CalledOnWrongThread : BaseIntrospectionRule
	{
		public CalledOnWrongThread()
			: base("CalledOnWrongThread", "Threading.FxCop", typeof(CalledOnWrongThread).Assembly)
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
						//if (!CallGraph.CallersFor(method).Any())
						//    Debug.WriteLine(String.Format("{0} is never called.", member.FullName));
						foreach (var caller in CallGraph.CallersFor(method))
						{
							var callThreadName = Utils.GetThreadName(caller);
							var callOnThreads = Utils.GetCallOnThreadList(Utils.GetBaseMember(caller));
							if (callThreadName.Length > 0)
							{
								if (!threads.Contains(callThreadName))
									this.Problems.Add(new Problem(GetResolution(member.FullName, String.Join(", ", threads), caller.FullName, callThreadName), member, member.FullName + ":CalledOnWrongThread:1"));
							}
							else if (callOnThreads.Length > 0)
							{
								if (callOnThreads.Any(t => !threads.Contains(t)))
									this.Problems.Add(new Problem(GetResolution(member.FullName, String.Join(", ", threads), caller.FullName, String.Join(", ", callOnThreads)), member, member.FullName + ":CalledOnWrongThread:2"));
							}
						}
					}
				}
			}
			return base.Problems;
		}
	}
}
