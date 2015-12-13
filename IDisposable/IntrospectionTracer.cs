//------------------------------------------------------------------------------
// http://james-ross.co.uk/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using System.Diagnostics;
using Microsoft.FxCop.Sdk;

namespace IDisposable {
	public class IntrospectionTracer : BaseIntrospectionRule {
		public IntrospectionTracer(string name, string resourceName, System.Reflection.Assembly resourceAssembly)
			: base(name, resourceName, resourceAssembly) {
		}

		public override ProblemCollection Check(Member member) {
			base.Visit(member);
			return base.Problems;
		}

		public override ProblemCollection Check(ModuleNode module) {
			base.Visit(module);
			return base.Problems;
		}

		public override ProblemCollection Check(Parameter parameter) {
			base.Visit(parameter);
			return base.Problems;
		}

		public override ProblemCollection Check(TypeNode type) {
			base.Visit(type);
			return base.Problems;
		}

		public override void Visit(Node node) {
			Debug.WriteLine(node);
			Debug.Indent();
			base.Visit(node);
			Debug.Unindent();
		}

		public override void VisitAddressDereference(AddressDereference dereference) {
			Debug.WriteLine(dereference);
			Debug.Indent();
			base.VisitAddressDereference(dereference);
			Debug.Unindent();
		}

		public override void VisitAssembly(AssemblyNode assembly) {
			Debug.WriteLine(assembly);
			Debug.Indent();
			base.VisitAssembly(assembly);
			Debug.Unindent();
		}

		public override void VisitAssemblyReference(AssemblyReference assemblyReference) {
			Debug.WriteLine(assemblyReference);
			Debug.Indent();
			base.VisitAssemblyReference(assemblyReference);
			Debug.Unindent();
		}

		public override void VisitAssignmentStatement(AssignmentStatement assignment) {
			Debug.WriteLine(assignment);
			Debug.Indent();
			base.VisitAssignmentStatement(assignment);
			Debug.Unindent();
		}

		public override void VisitAttributeConstructor(AttributeNode attribute) {
			Debug.WriteLine(attribute);
			Debug.Indent();
			base.VisitAttributeConstructor(attribute);
			Debug.Unindent();
		}

		public override void VisitAttributeNode(AttributeNode attribute) {
			Debug.WriteLine(attribute);
			Debug.Indent();
			base.VisitAttributeNode(attribute);
			Debug.Unindent();
		}

		public override void VisitAttributes(AttributeNodeCollection attributes) {
			Debug.WriteLine(attributes);
			Debug.Indent();
			base.VisitAttributes(attributes);
			Debug.Unindent();
		}

		public override void VisitBinaryExpression(BinaryExpression binaryExpression) {
			Debug.WriteLine(binaryExpression);
			Debug.Indent();
			base.VisitBinaryExpression(binaryExpression);
			Debug.Unindent();
		}

		public override void VisitBlock(Block block) {
			Debug.WriteLine(block);
			Debug.Indent();
			base.VisitBlock(block);
			Debug.Unindent();
		}

		public override void VisitBlocks(BlockCollection blocks) {
			Debug.WriteLine(blocks);
			Debug.Indent();
			base.VisitBlocks(blocks);
			Debug.Unindent();
		}

		public override void VisitBranch(Branch branch) {
			Debug.WriteLine(branch);
			Debug.Indent();
			base.VisitBranch(branch);
			Debug.Unindent();
		}

		public override void VisitCatch(CatchNode catchNode) {
			Debug.WriteLine(catchNode);
			Debug.Indent();
			base.VisitCatch(catchNode);
			Debug.Unindent();
		}

		public override void VisitCatchHandlers(CatchNodeCollection catchClauses) {
			Debug.WriteLine(catchClauses);
			Debug.Indent();
			base.VisitCatchHandlers(catchClauses);
			Debug.Unindent();
		}

		public override void VisitClass(ClassNode classType) {
			Debug.WriteLine(classType);
			Debug.Indent();
			base.VisitClass(classType);
			Debug.Unindent();
		}

		public override void VisitConstruct(Construct construct) {
			Debug.WriteLine(construct);
			Debug.Indent();
			base.VisitConstruct(construct);
			Debug.Unindent();
		}

		public override void VisitConstructArray(ConstructArray constructArray) {
			Debug.WriteLine(constructArray);
			Debug.Indent();
			base.VisitConstructArray(constructArray);
			Debug.Unindent();
		}

		public override void VisitDelegateNode(DelegateNode delegateNode) {
			Debug.WriteLine(delegateNode);
			Debug.Indent();
			base.VisitDelegateNode(delegateNode);
			Debug.Unindent();
		}

		public override void VisitEndFilter(EndFilter endFilter) {
			Debug.WriteLine(endFilter);
			Debug.Indent();
			base.VisitEndFilter(endFilter);
			Debug.Unindent();
		}

		public override void VisitEndFinally(EndFinally endFinally) {
			Debug.WriteLine(endFinally);
			Debug.Indent();
			base.VisitEndFinally(endFinally);
			Debug.Unindent();
		}

		public override void VisitEnumNode(EnumNode enumNode) {
			Debug.WriteLine(enumNode);
			Debug.Indent();
			base.VisitEnumNode(enumNode);
			Debug.Unindent();
		}

		public override void VisitEvent(EventNode eventMember) {
			Debug.WriteLine(eventMember);
			Debug.Indent();
			base.VisitEvent(eventMember);
			Debug.Unindent();
		}

		public override void VisitExpression(Expression expression) {
			Debug.WriteLine(expression);
			Debug.Indent();
			base.VisitExpression(expression);
			Debug.Unindent();
		}

		public override void VisitExpressions(ExpressionCollection expressions) {
			Debug.WriteLine(expressions);
			Debug.Indent();
			base.VisitExpressions(expressions);
			Debug.Unindent();
		}

		public override void VisitExpressionStatement(ExpressionStatement statement) {
			Debug.WriteLine(statement);
			Debug.Indent();
			base.VisitExpressionStatement(statement);
			Debug.Unindent();
		}

		public override void VisitFaultHandler(FaultHandler faultHandler) {
			Debug.WriteLine(faultHandler);
			Debug.Indent();
			base.VisitFaultHandler(faultHandler);
			Debug.Unindent();
		}

		public override void VisitField(Field field) {
			Debug.WriteLine(field);
			Debug.Indent();
			base.VisitField(field);
			Debug.Unindent();
		}

		public override void VisitFilter(Filter filter) {
			Debug.WriteLine(filter);
			Debug.Indent();
			base.VisitFilter(filter);
			Debug.Unindent();
		}

		public override void VisitFinally(FinallyNode finallyClause) {
			Debug.WriteLine(finallyClause);
			Debug.Indent();
			base.VisitFinally(finallyClause);
			Debug.Unindent();
		}

		public override void VisitIndexer(Indexer indexer) {
			Debug.WriteLine(indexer);
			Debug.Indent();
			base.VisitIndexer(indexer);
			Debug.Unindent();
		}

		public override void VisitInstanceInitializer(InstanceInitializer constructor) {
			Debug.WriteLine(constructor);
			Debug.Indent();
			base.VisitInstanceInitializer(constructor);
			Debug.Unindent();
		}

		public override void VisitInterface(InterfaceNode interfaceType) {
			Debug.WriteLine(interfaceType);
			Debug.Indent();
			base.VisitInterface(interfaceType);
			Debug.Unindent();
		}

		public override void VisitInterfaceReference(InterfaceNode interfaceType) {
			Debug.WriteLine(interfaceType);
			Debug.Indent();
			base.VisitInterfaceReference(interfaceType);
			Debug.Unindent();
		}

		public override void VisitInterfaceReferences(InterfaceCollection interfaceReferences) {
			Debug.WriteLine(interfaceReferences);
			Debug.Indent();
			base.VisitInterfaceReferences(interfaceReferences);
			Debug.Unindent();
		}

		public override void VisitLiteral(Literal literal) {
			Debug.WriteLine(literal);
			Debug.Indent();
			base.VisitLiteral(literal);
			Debug.Unindent();
		}

		public override void VisitLocal(Local local) {
			Debug.WriteLine(local);
			Debug.Indent();
			base.VisitLocal(local);
			Debug.Unindent();
		}

		public override void VisitMemberBinding(MemberBinding memberBinding) {
			Debug.WriteLine(memberBinding);
			Debug.Indent();
			base.VisitMemberBinding(memberBinding);
			Debug.Unindent();
		}

		public override void VisitMembers(MemberCollection members) {
			Debug.WriteLine(members);
			Debug.Indent();
			base.VisitMembers(members);
			Debug.Unindent();
		}

		public override void VisitMethod(Method method) {
			Debug.WriteLine(method);
			Debug.Indent();
			base.VisitMethod(method);
			Debug.Unindent();
		}

		public override void VisitMethodCall(MethodCall call) {
			Debug.WriteLine(call);
			Debug.Indent();
			base.VisitMethodCall(call);
			Debug.Unindent();
		}

		public override void VisitModule(ModuleNode module) {
			Debug.WriteLine(module);
			Debug.Indent();
			base.VisitModule(module);
			Debug.Unindent();
		}

		public override void VisitModuleReference(ModuleReference moduleReference) {
			Debug.WriteLine(moduleReference);
			Debug.Indent();
			base.VisitModuleReference(moduleReference);
			Debug.Unindent();
		}

		public override void VisitNamedArgument(NamedArgument namedArgument) {
			Debug.WriteLine(namedArgument);
			Debug.Indent();
			base.VisitNamedArgument(namedArgument);
			Debug.Unindent();
		}

		public override void VisitParameter(Parameter parameter) {
			Debug.WriteLine(parameter);
			Debug.Indent();
			base.VisitParameter(parameter);
			Debug.Unindent();
		}

		public override void VisitParameters(ParameterCollection parameters) {
			Debug.WriteLine(parameters);
			Debug.Indent();
			base.VisitParameters(parameters);
			Debug.Unindent();
		}

		public override void VisitProperty(PropertyNode property) {
			Debug.WriteLine(property);
			Debug.Indent();
			base.VisitProperty(property);
			Debug.Unindent();
		}

		public override void VisitReturn(ReturnNode returnInstruction) {
			Debug.WriteLine(returnInstruction);
			Debug.Indent();
			base.VisitReturn(returnInstruction);
			Debug.Unindent();
		}

		public override void VisitSecurityAttribute(SecurityAttribute attribute) {
			Debug.WriteLine(attribute);
			Debug.Indent();
			base.VisitSecurityAttribute(attribute);
			Debug.Unindent();
		}

		public override void VisitSecurityAttributes(SecurityAttributeCollection attributes) {
			Debug.WriteLine(attributes);
			Debug.Indent();
			base.VisitSecurityAttributes(attributes);
			Debug.Unindent();
		}

		public override void VisitStatements(StatementCollection statements) {
			Debug.WriteLine(statements);
			Debug.Indent();
			base.VisitStatements(statements);
			Debug.Unindent();
		}

		public override void VisitStaticInitializer(StaticInitializer classConstructor) {
			Debug.WriteLine(classConstructor);
			Debug.Indent();
			base.VisitStaticInitializer(classConstructor);
			Debug.Unindent();
		}

		public override void VisitStruct(Struct valueType) {
			Debug.WriteLine(valueType);
			Debug.Indent();
			base.VisitStruct(valueType);
			Debug.Unindent();
		}

		public override void VisitSwitchInstruction(SwitchInstruction switchInstruction) {
			Debug.WriteLine(switchInstruction);
			Debug.Indent();
			base.VisitSwitchInstruction(switchInstruction);
			Debug.Unindent();
		}

		public override void VisitTargetExpression(Expression expression) {
			Debug.WriteLine(expression);
			Debug.Indent();
			base.VisitTargetExpression(expression);
			Debug.Unindent();
		}

		public override void VisitTernaryExpression(TernaryExpression expression) {
			Debug.WriteLine(expression);
			Debug.Indent();
			base.VisitTernaryExpression(expression);
			Debug.Unindent();
		}

		public override void VisitThis(This thisVariable) {
			Debug.WriteLine(thisVariable);
			Debug.Indent();
			base.VisitThis(thisVariable);
			Debug.Unindent();
		}

		public override void VisitThrow(ThrowNode throwInstruction) {
			Debug.WriteLine(throwInstruction);
			Debug.Indent();
			base.VisitThrow(throwInstruction);
			Debug.Unindent();
		}

		public override void VisitTry(TryNode tryClause) {
			Debug.WriteLine(tryClause);
			Debug.Indent();
			base.VisitTry(tryClause);
			Debug.Unindent();
		}

		public override void VisitTypeModifier(TypeModifier typeModifier) {
			Debug.WriteLine(typeModifier);
			Debug.Indent();
			base.VisitTypeModifier(typeModifier);
			Debug.Unindent();
		}

		public override void VisitTypeNode(TypeNode type) {
			Debug.WriteLine(type);
			Debug.Indent();
			base.VisitTypeNode(type);
			Debug.Unindent();
		}

		public override void VisitTypeNodes(TypeNodeCollection types) {
			Debug.WriteLine(types);
			Debug.Indent();
			base.VisitTypeNodes(types);
			Debug.Unindent();
		}

		public override void VisitTypeParameter(TypeNode typeParameter) {
			Debug.WriteLine(typeParameter);
			Debug.Indent();
			base.VisitTypeParameter(typeParameter);
			Debug.Unindent();
		}

		public override void VisitTypeParameters(TypeNodeCollection typeParameters) {
			Debug.WriteLine(typeParameters);
			Debug.Indent();
			base.VisitTypeParameters(typeParameters);
			Debug.Unindent();
		}

		public override void VisitTypeReference(TypeNode type) {
			Debug.WriteLine(type);
			Debug.Indent();
			base.VisitTypeReference(type);
			Debug.Unindent();
		}

		public override void VisitTypeReferences(TypeNodeCollection typeReferences) {
			Debug.WriteLine(typeReferences);
			Debug.Indent();
			base.VisitTypeReferences(typeReferences);
			Debug.Unindent();
		}

		public override void VisitUnaryExpression(UnaryExpression unaryExpression) {
			Debug.WriteLine(unaryExpression);
			Debug.Indent();
			base.VisitUnaryExpression(unaryExpression);
			Debug.Unindent();
		}
	}
}
