// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;
using System.Collections.Specialized;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;

namespace MathExpr
{
	public abstract class Functor
	{
		public abstract float evaluate(float x);
	}

	public class Compiler : NodeVisitorImpl
	{		
		public override void VisitLiteral(LiteralNode n) 
		{
			methodIL.Emit(OpCodes.Ldc_R4, n.Value);
		}

		public override void VisitSymbol(SymbolNode n) 
		{
			// actually just one parameter
			methodIL.Emit(OpCodes.Ldarg_1);
		}

		public override void VisitAddSub(AddSubNode n) 
		{
			n.Left.Accept(this);
			n.Right.Accept(this);
			methodIL.Emit(n.IsAddition ? OpCodes.Add : OpCodes.Sub);
		}

		public override void VisitMulDiv(MulDivNode n) 
		{
			n.Left.Accept(this);
			n.Right.Accept(this);
			methodIL.Emit(n.IsMultiplication ? OpCodes.Mul : OpCodes.Div);		
		}

		public override void VisitPow(PowNode n) 		
		{
			n.Left.Accept(this);
			methodIL.Emit(OpCodes.Conv_R8);
			n.Right.Accept(this);
			methodIL.Emit(OpCodes.Conv_R8);
			methodIL.EmitCall(OpCodes.Call, typeof(Math).GetMethod("Pow"), null);
			methodIL.Emit(OpCodes.Conv_R4);		
		}

		public override void VisitNegate(NegateNode n) 
		{
			n[0].Accept(this);
			methodIL.Emit(OpCodes.Neg);
		}

		public override void VisitFx(FxNode n) 
		{
			n[0].Accept(this);
			MethodInfo mi = null;
			bool doublemeth = true;
			switch(n.Fx)
			{
				case FxNode.FxType.Cos: mi = typeof(Math).GetMethod("Cos"); break;
				case FxNode.FxType.Sin: mi = typeof(Math).GetMethod("Sin"); break;
				case FxNode.FxType.Ln: mi = typeof(Math).GetMethod("Log");  break;
				default:
					break;					
			}
			//mi = typeof(TxMath).GetMethod("AZZ");
			if(mi == null) return;
			
			if(doublemeth) 			
				methodIL.Emit(OpCodes.Conv_R8);			
			methodIL.EmitCall(OpCodes.Call, mi, null);
			if(doublemeth) 			
				methodIL.Emit(OpCodes.Conv_R4);					
		}			
		
		public override void VisitRelational(RelationalNode n) 
		{
			n.Left.Accept(this);
			n.Right.Accept(this);		
			OpCode op;

			switch(n.Op)
			{
				case RelationalNode.Operation.Eq: op = OpCodes.Beq; break;
				case RelationalNode.Operation.Gt: op = OpCodes.Bgt; break;
				case RelationalNode.Operation.Lt: op = OpCodes.Blt; break;
				case RelationalNode.Operation.Lte: op = OpCodes.Ble; break;
				case RelationalNode.Operation.Gte: op = OpCodes.Bge; break;
				case RelationalNode.Operation.Neq: op = OpCodes.Bne_Un; break;
				default:
					return;
			}

			Label label = methodIL.DefineLabel();
			Label labelE = methodIL.DefineLabel();

			methodIL.Emit(op, label);
			methodIL.Emit(OpCodes.Ldc_I4_0);
			methodIL.Emit(OpCodes.Br, labelE);
			methodIL.MarkLabel(label);
			methodIL.Emit(OpCodes.Ldc_I4_1);
			methodIL.MarkLabel(labelE);
			methodIL.Emit(OpCodes.Conv_R4);			
		}

		public override void VisitLogic(LogicNode n) 
		{
			n.Left.Accept(this);
			Label labelE = methodIL.DefineLabel();
			Label label = methodIL.DefineLabel();

			if(n.Op == LogicNode.Operation.Or) 
			{
				methodIL.Emit(OpCodes.Ldc_R4, 0);
				methodIL.Emit(OpCodes.Bne_Un, label);

				n.Right.Accept(this);		
				methodIL.Emit(OpCodes.Ldc_R4, 0);
				methodIL.Emit(OpCodes.Bne_Un, label);
				methodIL.Emit(OpCodes.Ldc_I4_0);
				methodIL.Emit(OpCodes.Br, labelE);
				methodIL.MarkLabel(label);
				methodIL.Emit(OpCodes.Ldc_I4_1);
				methodIL.MarkLabel(labelE);
				methodIL.Emit(OpCodes.Conv_R4);
			}
			else
			{				
				Label label2 = methodIL.DefineLabel();
				methodIL.Emit(OpCodes.Ldc_R4, 0);
				methodIL.Emit(OpCodes.Beq, label);

				n.Right.Accept(this);		
				methodIL.Emit(OpCodes.Ldc_R4, 0);
				methodIL.Emit(OpCodes.Bne_Un, label2);

				methodIL.MarkLabel(label);
				methodIL.Emit(OpCodes.Ldc_I4_0);
				methodIL.Emit(OpCodes.Br, labelE);
				
				methodIL.MarkLabel(label2);
				methodIL.Emit(OpCodes.Ldc_I4_1);

				methodIL.MarkLabel(labelE);
				methodIL.Emit(OpCodes.Conv_R4);
			}
		}

		public override void VisitLogicNot(LogicNotNode n) 
		{
			n[0].Accept(this);			
			Label labelE = methodIL.DefineLabel();
			Label label = methodIL.DefineLabel();
			methodIL.Emit(OpCodes.Ldc_R4, 0);
			methodIL.Emit(OpCodes.Bne_Un, label);
			methodIL.MarkLabel(label);
			methodIL.Emit(OpCodes.Ldc_I4_1);
			methodIL.MarkLabel(labelE);
			methodIL.Emit(OpCodes.Conv_R4);		
		}

		public Functor Compile(Node n)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = "EmittedAssembly";
			AssemblyBuilder assembly = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			ModuleBuilder module;
			module = assembly.DefineDynamicModule("EmittedModule");
			TypeBuilder helloWorldClass = module.DefineType("HelloWorld", TypeAttributes.Public, typeof(Functor));						
			Type[] constructorArgs = { };
			ConstructorBuilder constructor = helloWorldClass.DefineConstructor(
				MethodAttributes.Public, CallingConventions.Standard, null);
				
			ILGenerator constructorIL = constructor.GetILGenerator();
			constructorIL.Emit(OpCodes.Ldarg_0);
			ConstructorInfo superConstructor = typeof(Object).GetConstructor(new Type[0]);
			constructorIL.Emit(OpCodes.Call, superConstructor);
			constructorIL.Emit(OpCodes.Ret);

			Type [] args = { typeof(float) };
			MethodBuilder fxMethod = helloWorldClass.DefineMethod("evaluate", MethodAttributes.Public|MethodAttributes.Virtual , typeof(float), args);
			methodIL = fxMethod.GetILGenerator();			
			n.Accept(this);
			methodIL.Emit(OpCodes.Ret);
			Type dt = helloWorldClass.CreateType();
			//assembly.Save("x.exe");
			return (Functor)Activator.CreateInstance(dt, new Object[] { });

		}

		ILGenerator methodIL;
	}

	// test to show when AZZ is called
	public class TxMath
	{
		public static double AZZ(double d)
		{
			Console.Out.WriteLine("AZZ " + d);
			return Math.Cos(d);
		}
	}


}