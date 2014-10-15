// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;

namespace MathExpr
{
	/// <summary>
	/// a Stack of Floats to implement the evaluation stack
	/// (because we use the Visitor Pattern)
	/// </summary>
	public class FloatStack 
	{
		public FloatStack(int size)
		{
			data = new float[size];
			top  = 0;
		}

		public void Push(float f)
		{
			data[top++] = f;			
		}

		public float Pop()
		{
			return data[--top];
		}

		public float Top
		{
			get { return data[top-1]; }
			set { data[top-1] = value; }
		}

		public void Clear()
		{
			top = 0;
		}

		float [] data;
		int top; 
	}


	/// <summary>
	/// A Node Visitor that Implements a recursive Evaluator
	/// We have a method for each type of Node
	/// </summary>
	public class Evaluator : NodeVisitorImpl
	{
		public Evaluator()
		{
			table = new SymbolTable();
		}

		public Evaluator(SymbolTable st)
		{
			table = st;
		}

		public float this[string n]
		{
			get {
				object o = table.GetSymbolValue(n);
				return o == null ? 0 : (float)o;
			}
			set {
				table.SetSymbolValue(n, value);
			}
		}

		public override void VisitLiteral(LiteralNode n) 
		{
			stack.Push(n.Value);
		}

		public override void VisitSymbol(SymbolNode n) 
		{
			object o = table.GetSymbolValue(n.Symbol);			
			stack.Push((float)o);
		}

		public override void VisitAddSub(AddSubNode n) 
		{
			n.Left.Accept(this);
			n.Right.Accept(this);		
			float l = stack.Pop();
			stack.Top = stack.Top + (n.IsAddition ? l : -l);
		}

		public override void VisitMulDiv(MulDivNode n) 
		{
			n.Left.Accept(this);
			n.Right.Accept(this);		
			float r = stack.Pop();
			if(n.IsMultiplication)
				stack.Top = stack.Top * r;
			else
				stack.Top = stack.Top / r;
		}

		public override void VisitPow(PowNode n)
		{
			n.Left.Accept(this);
			n.Right.Accept(this);		
			float l = stack.Pop();
			stack.Top = (float)Math.Pow(stack.Top, l);
		}

		public override void VisitNegate(NegateNode n)
		{
			n[0].Accept(this);
			stack.Top = - stack.Top;
		}

		public override void VisitFx(FxNode n)
		{
			n[0].Accept(this);
			float f = stack.Top;

			switch(n.Fx)
			{
				case FxNode.FxType.Cos: f = (float)Math.Cos(f); break;
				case FxNode.FxType.Sin: f = (float)Math.Sin(f); break;
				case FxNode.FxType.Ln: f =(float)Math.Log(f); break;
				case FxNode.FxType._Custom:
					// find function in symbol table
					MathExpr.CustomFX fx = table.GetFunction(n.Name);
					if(fx != null)
					{
						f = (float)fx(f);
					}
					break;
				default:  break;					
			}
			stack.Top = f;
		}

		public override void VisitRelational(RelationalNode n) 
		{
			n.Left.Accept(this);
			n.Right.Accept(this);		
			float r = stack.Pop();			
			bool br;
			switch(n.Op)
			{
				case RelationalNode.Operation.Eq: br = stack.Top == r; break;
				case RelationalNode.Operation.Gt: br = stack.Top > r; break;
				case RelationalNode.Operation.Lt: br = stack.Top < r; break;
				case RelationalNode.Operation.Lte: br = stack.Top <= r; break;
				case RelationalNode.Operation.Gte: br = stack.Top >= r; break;
				case RelationalNode.Operation.Neq: br = stack.Top != r; break;
				default:
					br = false;
					break;
			}
			stack.Top = br ? 1 : 0;
		}

		public override void VisitLogic(LogicNode n) 
		{
			n.Left.Accept(this);
			n.Right.Accept(this);		
			float r = stack.Pop();			
			bool br;
			switch(n.Op)
			{
				case LogicNode.Operation.And: br = stack.Top != 0 && r != 0; break;
				case LogicNode.Operation.Or: br = stack.Top != 0 || r != 0; break;
				default:
					br = false;
					break;
			}
			stack.Top = br ? 1 : 0;
		}

		public override void VisitLogicNot(LogicNotNode n) 
		{
			stack.Top = stack.Top != 0 ? 0 : 1;
		}


		public float Evaluate(Node n)
		{
			stack.Clear();
			n.Accept(this);
			return stack.Top;
		}

		FloatStack stack = new FloatStack(100);
		SymbolTable table;
	}

	
}