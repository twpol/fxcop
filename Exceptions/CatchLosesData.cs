//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.FxCop.Sdk;

namespace Exceptions {
	public class CatchLosesData : BaseIntrospectionRule {
		public CatchLosesData()
			: base("CatchLosesData", "Exceptions.FxCop", typeof(CatchLosesData).Assembly) {
		}

		public override ProblemCollection Check(Member member) {
			//var property = member as PropertyNode;
			//var method = member as Method;
			//if (property != null) {
			//    if (property.Getter != null) base.Visit(property.Getter);
			//    if (property.Setter != null) base.Visit(property.Setter);
			//} else if (method != null) {
			//    base.Visit(method);
			//}
			base.Visit(member);
			return base.Problems;
		}

		List<Variable> CatchVariable;
		public override void BeforeAnalysis() {
			base.BeforeAnalysis();
			CatchVariable = new List<Variable>();
		}

		bool gotType;
		bool gotMessage;
		bool gotInnerException;
		public override void VisitCatch(CatchNode catchNode) {
			CatchVariable.Clear();
			CatchVariable.Add(catchNode.Variable as Variable);
			gotType = false;
			gotMessage = false;
			gotInnerException = false;
			base.VisitCatch(catchNode);
			if (!gotInnerException && (!gotType || !gotMessage)) {
				var variable = ((Variable)catchNode.Variable).Name.Name;
				if (CatchVariable.Any(v => !v.Name.Name.StartsWith("$"))) {
					variable = CatchVariable.First(v => !v.Name.Name.StartsWith("$")).Name.Name;
				}
				if (!gotType && !gotMessage) {
					this.Problems.Add(new Problem(GetNamedResolution("All", variable), catchNode, variable));
				} else if (!gotType) {
					this.Problems.Add(new Problem(GetNamedResolution("Type", variable), catchNode, variable + ":Type"));
				} else if (!gotMessage) {
					this.Problems.Add(new Problem(GetNamedResolution("Message", variable), catchNode, variable + ":Message"));
				}
			}
			CatchVariable.Clear();
		}

		public override void VisitAssignmentStatement(AssignmentStatement assignment) {
			if (assignment.Target is Variable) {
				if (CatchVariable.Contains(assignment.Source as Variable)) {
					CatchVariable.Add(assignment.Target as Variable);
				} else if (CatchVariable.Contains(assignment.Target as Variable)) {
					CatchVariable.Remove(assignment.Target as Variable);
				}
			}
			base.VisitAssignmentStatement(assignment);
		}

		public override void VisitMemberBinding(MemberBinding memberBinding) {
			if (CatchVariable.Contains(memberBinding.TargetObject as Variable)) {
				if (memberBinding.BoundMember.Name.Name == "GetType") {
					gotType = true;
				} else if (memberBinding.BoundMember.Name.Name == "get_Message") {
					gotMessage = true;
				} else if (memberBinding.BoundMember.Name.Name == "ToString") {
					gotType = true;
					gotMessage = true;
				}
			}
			base.VisitMemberBinding(memberBinding);
		}

		bool foundCatchVariable;
		public override void VisitConstruct(Construct construct) {
			if ((CatchVariable.Count > 0) && (construct.Type.IsAssignableTo(FrameworkTypes.Exception))) {
				foundCatchVariable = false;
				base.VisitConstruct(construct);
				if (foundCatchVariable) {
					gotInnerException = true;
				}
			} else {
				base.VisitConstruct(construct);
			}
		}

		public override void VisitLocal(Local local) {
			if (CatchVariable.Contains(local)) {
				foundCatchVariable = true;
			}
			base.VisitLocal(local);
		}
	}
}
