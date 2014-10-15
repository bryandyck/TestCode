// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;
using System.Collections.Specialized;

namespace MathExpr
{
	/// <summary>
	/// Derive a Node expression
	/// </summary>
	public class Derivator : NodeVisitorImpl
	{
		public Node Derive(Node n, string devsym)
		{
			derivationSymbol = devsym;
			stack.Clear();
			n.Accept(this);
			return stack.Top;
		}

		public override void VisitLiteral(LiteralNode n) 
		{
			stack.Push(NilNode.Nil);
		}
		
		public override void VisitSymbol(SymbolNode n) 
		{
			stack.Push(n.Symbol == derivationSymbol ? (Node)LiteralNode.One : NilNode.Nil);
		}
		
		public override void VisitAddSub(AddSubNode n) 
		{
			// check if c.DeriveSymbol == Symbol
			n.Left.Accept(this);
			Node ln = stack.Pop();
			n.Right.Accept(this);
			Node rn = stack.Pop();
			int how = (ln == NilNode.Nil ? 1 : 0)|(rn == NilNode.Nil ? 2 : 0);

			// check
			if((how & 1) == 0 && ln is LiteralNode && ((LiteralNode)ln).Value == 0) 
				how |= 1;

			if((how & 2) == 0 && rn is LiteralNode && ((LiteralNode)rn).Value == 0) 
				how |= 2;
			
			Node r;
			switch(how)
			{
				case 0:
					r = new AddSubNode(ln,rn, n.IsAddition); break;
				case 1:
					r = !n.IsAddition ? new NegateNode(rn) : rn; break;
				case 2:
					r = ln; break;
				case 3:
					r = NilNode.Nil; break;		// same as 0
				default:
					r = null; break;
			}		
			stack.Push(r);
		}
		
		public override void VisitMulDiv(MulDivNode n) 
		{
			n.Left.Accept(this);
			Node ln = stack.Pop();
			n.Right.Accept(this);
			Node rn = stack.Pop();
			Node r;

			int how = (ln == NilNode.Nil ? 1 : 0)|(rn == NilNode.Nil ? 2 : 0);
			int mulnull = 0;

			// result is alway zero
			if(how == 3) 
			{ 
				stack.Push(NilNode.Nil); 
				return; 
			}

			if((how & 2) == 0 && rn is LiteralNode && ((LiteralNode)rn).Value == 1) 
				mulnull |= 2;

			if((how & 1) == 0 && ln is LiteralNode && ((LiteralNode)ln).Value == 1) 
				mulnull |= 1;

			// f'*g + f*g'	
			if(n.IsMultiplication) 
			{
				// check if c.DeriveSymbol == Symbol
				switch(how) 
				{
					case 0:											
						r = new AddSubNode((mulnull & 1) == 1 ? n.Right : new MulDivNode(ln, n.Right, true),
							(mulnull & 2) == 2? n.Left : new MulDivNode(n.Left, rn, true), true);						
						break;
					case 1:
						r = (mulnull & 2) == 2? n.Left : new MulDivNode(n.Left, rn, true);						
						break;
					case 2:
						r = (mulnull & 1) == 1? n.Right : new MulDivNode(ln, n.Right, true);						
						break;
					default:
						r = null;
						break;
				}
			}
			else 
			{
				// f /g => (f' g - f*g')/g^2
				switch(how) 
				{
					case 0:
						r = new MulDivNode(new AddSubNode ((mulnull & 1) == 1? n.Right : new MulDivNode(ln, n.Right, true),
							(mulnull & 2) == 2? n.Left : new MulDivNode(n.Left, rn, true), false), new MulDivNode(n.Right, n.Right, true), false);
						break;
					case 1:
						r = (mulnull & 2) == 2? (Node) new NegateNode(new MulDivNode(n.Left, new MulDivNode(n.Right, n.Right, true), false)):
							(Node)new MulDivNode(new NegateNode (new MulDivNode(n.Left, rn, true)), new MulDivNode(n.Right, n.Right, true), false);
						break;
					case 2:
						r = (mulnull & 1) == 1? new MulDivNode(LiteralNode.One, n.Right, false) : new MulDivNode(ln, n.Right, false);
						break;
					default:
						r = null;
						break;
				}
				
			}			
			stack.Push(r);		
		}
		
		// x^n is nx^(n-1)		
		// a generic f(x)^g(x) it's quite complicate:
		//   f(x)^g(x) = exp(ln(fx(x)^g(x))) = exp(g(x)*ln(f(x)))
		//   d/dx exp(g(x)*ln(f(x))) = exp(g(x)*ln(f(x))) * d/dx g(x)*ln(f(x)) =
		//   = f(x)^g(x) * (g(x)*f'(x)/f(x)+g'(x)*ln(f(x)))
		//   on g'(x) == 1 then ... f(x)^g(x)*(g(x)/f(x)+ln x)
		//   on g'(x) == 0 then ... f(x)^g(x)*(g(x)/f(x))
		//	 on f'(x) == 0 then ... f(x)^g(x)*(g'(x)*ln(f(x)))
		//	 on f'(x) == 1 then ... f(x)^g(x) * (g(x)/f(x)+g'(x)*ln(f(x)))
		//
		// When f(x) is x then: x^g(x) * (g(x)/x+g'(x)ln x)
		//   on g'(x) == 1 then ... orig*(g(x)/x+ln x)
		//   on g'(x) == 0 then ... x^g(x)*(g(x)/x) = x^(g(x)-1) * g(x)
		// specifically when g(x) is n we have x^n * (n/x) = n * x^(n-1)		
		//
		// When g(x) is a literal then: d/dx f(x)^n = n*(f(x)*(n-1))*f'(x)		
		public override void VisitPow(PowNode n) 
		{
			Node r = null;

			if(n.Left is SymbolNode && ((SymbolNode)n.Left).Symbol.Equals(derivationSymbol))
			{
				n.Right.Accept(this);
				Node rn = stack.Pop();

				// check for numeric exponent
				if(n.Right is LiteralNode) 
				{
					float v = ((LiteralNode)n.Right).Value;
					r = n.Right * (n.Left ^ (v-1));
					//return new MulDivNode(new LiteralNode(n), new PowNode(n.Left, new LiteralNode(n-1)), true);	
				}
				else 
				{
					// zero => x^(g(x)-1) * g(x)
					// one  => orig*(g(x)/x+ln x)
					// else => x^g(x) * (g(x)/x+g'(x)ln x)
					if(rn == NilNode.Nil || (rn is LiteralNode && ((LiteralNode)rn).Value == 0))												
						r = (n.Left ^ (n.Right - 1)) * n.Right;
					else if(rn is LiteralNode && ((LiteralNode)rn).Value == 1) 
						r = n * (n.Right / n.Left + Node.Log(n.Left));	
					else
						r = n * (n.Right / n.Left + rn * Node.Log(n.Left));
					// efficient versions
					//return new MulDivNode(this,new AddSubNode(new MulDivNode(n.Right, n.Left, false), new FxNode(FxNode.FxType.Ln, n.Left), true), true);
					//return new MulDivNode(new MulDivNode(this, new PowNode(n.Left, new AddSubNode(n.Right,new LiteralNode(1), false)), true) , n.Right, true);
					//return new MulDivNode(this,new AddSubNode(new MulDivNode(n.Right, n.Left, false), new MulDivNode(rn, new FxNode(FxNode.FxType.Ln, n.Left), true), true), true);
				}
			}
			else if(n.Right is LiteralNode) 
			{
				// d/dx f(x)^n = n*(f(x)^(n-1))*f'(x)				
				// on n = 0 => f(x)^0 = 1 => 0
				// on n = 1 => f(x)^1 = f(x) => f(x)'
				// on n = 2 => f(x)^2 = 2 f(x) * f'(x)				
				n.Left.Accept(this);
				Node ln = stack.Pop();			
				float vn = ((LiteralNode)n.Right).Value;

				if(vn == 0) 
					r = NilNode.Nil;
				else if(vn == 1) 
					r = ln;
				else if(vn == 2) 
					r = 2 * n.Left * ln;
				else					
					r = n.Right * (n.Left ^ (vn-1)) * ln;				

				// efficient versions:
				//	return new MulDivNode(n.Right, new MulDivNode(n.Left, ln, true), true);
				//  return new MulDivNode(new MulDivNode(n.Right, new PowNode(n.Left, new LiteralNode(n-1)), true), ln, true);
			}

			else 
			{
				// f(x)^g(x) * (g(x)*f'(x)/f(x)+g'(x)*ln(f(x)))
				n.Left.Accept(this);
				Node rn = stack.Pop();			
				n.Right.Accept(this);
				Node ln = stack.Pop();			
				//return new MulDivNode(this, new AddSubNode(new MulDivNode(n.Right, new MulDivNode(ln, n.Left, false), true),
				//	new MulDivNode(rn, new FxNode(FxNode.FxType.Ln, n.Left), true), true), true);
				r = n * (n.Right * ln) + rn * Node.Log(n.Left);
			}		
			stack.Push(r);
		}
		
		public override void VisitNegate(NegateNode n) 
		{
			if(n[0] is LiteralNode) 
			{
				stack.Push(NilNode.Nil);
			}
			else 
			{
				n[0].Accept(this);							
				stack.Top = new NegateNode(stack.Top);
			}			
		}

		public override void VisitFx(FxNode n) 
		{
			n[0].Accept(this);
			if(stack.Top == NilNode.Nil) return;
			Node cn = stack.Pop();
			bool childone = (cn is LiteralNode) && ((LiteralNode)cn).Value == 1;
			Node r = null;
			switch(n.Fx)
			{
				case FxNode.FxType.Cos: r = new NegateNode(new FxNode(FxNode.FxType.Sin, n[0])) ; break;
				case FxNode.FxType.Sin: r = new FxNode(FxNode.FxType.Cos, n[0]); break;
				case FxNode.FxType.Ln:  r = new MulDivNode(LiteralNode.One, n[0], false); break; 
				case FxNode.FxType._Custom:
					throw new ExpressionNotDerivable(n);
				default:
					break;
			}	
			stack.Push(childone ? r : new MulDivNode(r, cn, true));
		}	

		public override void VisitRelational(RelationalNode n) 
		{
			throw new ExpressionNotDerivable(n);
		}

		public override void VisitLogic(LogicNode n) 
		{
			throw new ExpressionNotDerivable(n);
		}

		public override void VisitLogicNot(LogicNotNode n) 
		{
			throw new ExpressionNotDerivable(n);
		}
				
		string derivationSymbol;
		NodeStack stack = new NodeStack(100);
	}

	/// <summary>
	/// A NodeStack to implement the Derivator
	/// </summary>
	public class NodeStack
	{
		public NodeStack(int size)
		{
			data = new Node[size];
			top  = 0;
		}

		public void Push(Node f)
		{
			data[top++] = f;			
		}

		public Node Pop()
		{
			return data[--top];
		}

		public Node Top
		{
			get { return data[top-1]; }
			set { data[top-1] = value; }
		}

		public void Clear()
		{
			top = 0;
		}

		Node [] data;
		int top; 
	}

	public class ExpressionNotDerivable : Exception
	{
		public ExpressionNotDerivable(Node n)
		{
			target = n;
		}

		Node ErrorNode
		{
			get { return target; }
		}

		Node target;
	}
	
}
