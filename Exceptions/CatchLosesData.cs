//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: Microsoft Public License (Ms-PL).
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.FxCop.Sdk;

namespace Exceptions {
	public class CatchLosesData : BaseIntrospectionRule {
		public CatchLosesData()
			: base("CatchLosesData", "Exceptions.FxCop", typeof(CatchLosesData).Assembly) {
		}

		List<Variable> CatchVariable;
		public override void BeforeAnalysis() {
			base.BeforeAnalysis();
			CatchVariable = new List<Variable>();
		}

		public override ProblemCollection Check(Member member) {
			base.Visit(member);
			return base.Problems;
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
				// Check for one of our catch variables being assigned to
				// another. The C# compiler likes to do this, with pseudo-code:
				//   } catch (FooException $exception0) {
				//     e = $exception0;
				// TODO: Proper data tainting (especially flow control).
				if (CatchVariable.Contains(assignment.Source as Variable)) {
					CatchVariable.Add(assignment.Target as Variable);
				}
			}
			base.VisitAssignmentStatement(assignment);
		}

		public override void VisitConstruct(Construct construct) {
			if ((CatchVariable.Count > 0) && (construct.Type.IsAssignableTo(FrameworkTypes.Exception))) {
				// Someone's constructing an Exception-derived type, so let's
				// see if any of the expressions in the arguments access one
				// of our catch variables.
				visitedLocalCatchVariable = false;
				base.VisitConstruct(construct);
				if (visitedLocalCatchVariable) {
					gotInnerException = true;
				}
			} else {
				base.VisitConstruct(construct);
			}
		}

		public override void VisitMemberBinding(MemberBinding memberBinding) {
			if (CatchVariable.Contains(memberBinding.TargetObject as Variable)) {
				// We're binding a method to one of our catch variables - check
				// what method it is! We're going to do this the really cheaty
				// way and just pick on their string names.
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

		public override void VisitMethodCall(MethodCall call) {
			if (CatchVariable.Count > 0) {
				var binding = call.Callee as MemberBinding;
				Debug.Assert(binding != null, "MethodCall.Callee is not a MemberBinding.");
				var method = binding.BoundMember as Method;
				Debug.Assert(method != null, "MethodCall.Callee.BoundMember is not a Method.");
				// These are the same as calling base.VisitMethodCall(call),
				// but we'd like to do each operand individually so we can
				// trace into the method and correctly taint the argument.
				base.VisitExpression(call.Callee);
				if (call.Operands != null) {
					for (var index = 0; index < call.Operands.Count; index++) {
						visitedLocalCatchVariable = false;
						this.VisitExpression(call.Operands[index]);
						if (visitedLocalCatchVariable) {
							// We temporarily replace the list of catch variables
							// with the argument. This can nest fine.
							var oldVariable = CatchVariable;
							CatchVariable = new List<Variable>();
							Debug.Assert(method.Parameters[index].ArgumentListIndex == index, "Argument indexes are confusing!");
							CatchVariable.Add(method.Parameters[index]);
							base.Visit(binding.BoundMember);
							CatchVariable = oldVariable;
						}
					}
				}
				base.VisitTypeReference(call.Constraint);
			} else {
				base.VisitMethodCall(call);
			}
		}

		bool visitedLocalCatchVariable;
		public override void VisitLocal(Local local) {
			if (CatchVariable.Contains(local)) {
				visitedLocalCatchVariable = true;
			}
			base.VisitLocal(local);
		}
	}
}
