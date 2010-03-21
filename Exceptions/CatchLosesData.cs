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

		// We keep a list of all variables "in play" (i.e. containing the exception).
		List<Variable> CatchVariable;

		public override void BeforeAnalysis() {
			base.BeforeAnalysis();
			CatchVariable = new List<Variable>();
		}

		public override ProblemCollection Check(Member member) {
			base.Visit(member);
			return base.Problems;
		}

		bool VisitFoundExceptionGetType;
		bool VisitFoundExceptionMessage;
		bool VisitFoundInnerExceptionUse;
		public override void VisitCatch(CatchNode catchNode) {
			// Reset everything for the new catch block we've got to.
			// TODO: Work with nested catch blocks, if they're allowed.
			CatchVariable.Clear();
			CatchVariable.Add(catchNode.Variable as Variable);
			VisitFoundExceptionGetType = false;
			VisitFoundExceptionMessage = false;
			VisitFoundInnerExceptionUse = false;

			base.VisitCatch(catchNode);

			// We need the code to use the exception as an inner exception OR
			// access BOTH the type and message. Anything less triggers us.
			if (!VisitFoundInnerExceptionUse && (!VisitFoundExceptionGetType || !VisitFoundExceptionMessage)) {
				// Find a suitable variable to display to the user. Start with
				// the catch block's variable, but in C# this is often
				// $exception0 so we'll also look for the first CatchVariable
				// not starting $ and pick that in preference.
				var variable = ((Variable)catchNode.Variable).Name.Name;
				if (CatchVariable.Any(v => !v.Name.Name.StartsWith("$"))) {
					variable = CatchVariable.First(v => !v.Name.Name.StartsWith("$")).Name.Name;
				}

				// Check what we found/didn't find and output a single problem
				// for the catch block. We don't want multiple issues with a
				// single catch block.
				if (!VisitFoundExceptionGetType && !VisitFoundExceptionMessage) {
					this.Problems.Add(new Problem(GetNamedResolution("All", variable), catchNode, variable));
				} else if (!VisitFoundExceptionGetType) {
					this.Problems.Add(new Problem(GetNamedResolution("Type", variable), catchNode, variable + ":Type"));
				} else if (!VisitFoundExceptionMessage) {
					this.Problems.Add(new Problem(GetNamedResolution("Message", variable), catchNode, variable + ":Message"));
				}
			}

			// End of the catch block, so clear our data. This shouldn't
			// matter but it's safe.
			CatchVariable.Clear();
		}

		public override void VisitAssignmentStatement(AssignmentStatement assignment) {
			var sourceVar = assignment.Source as Variable;
			var targetVar = assignment.Target as Variable;
			if (targetVar != null) {
				// Check for one of our catch variables being assigned to
				// another. The C# compiler likes to do this (pseudo-code):
				//   } catch (FooException $exception0) {
				//     e = $exception0;
				// TODO: Proper data tainting (especially flow control).

				// Assignment TO an interesting variable removes the target from play.
				if (CatchVariable.Contains(targetVar)) {
					CatchVariable.Remove(targetVar);
				}

				// Assignment FROM an interesting variable adds the target to play.
				if (CatchVariable.Contains(sourceVar)) {
					CatchVariable.Add(targetVar);
				}
			}
			base.VisitAssignmentStatement(assignment);
		}

		bool VisitFoundCatchVariable;
		public override void VisitConstruct(Construct construct) {
			if ((CatchVariable.Count > 0) && (construct.Type.IsAssignableTo(FrameworkTypes.Exception))) {
				// Someone's constructing an Exception-derived type, so let's
				// see if any of the expressions in the arguments access one
				// of our catch variables.
				VisitFoundCatchVariable = false;
				base.VisitConstruct(construct);
				if (VisitFoundCatchVariable) {
					VisitFoundInnerExceptionUse = true;
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
					VisitFoundExceptionGetType = true;
				} else if (memberBinding.BoundMember.Name.Name == "get_Message") {
					VisitFoundExceptionMessage = true;
				} else if (memberBinding.BoundMember.Name.Name == "ToString") {
					VisitFoundExceptionGetType = true;
					VisitFoundExceptionMessage = true;
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
						VisitFoundCatchVariable = false;
						this.VisitExpression(call.Operands[index]);
						if (VisitFoundCatchVariable) {
							// We temporarily replace the list of catch variables
							// with the argument. This can nest fine.
							var oldVariable = CatchVariable;
							CatchVariable = new List<Variable>();
							Debug.Assert(method.Parameters[index].ParameterListIndex == index, "Parameter index doesn't match!");
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

		public override void VisitLocal(Local local) {
			if (CatchVariable.Contains(local)) {
				VisitFoundCatchVariable = true;
			}
			base.VisitLocal(local);
		}
	}
}
