// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
//
// This document contains the basic Node classes
using System;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Specialized;

namespace MathExpr
{
	/// <summary>
	/// Interface that every Visitor should implement
	/// </summary>
	public interface NodeVisitor
	{
		void VisitNode(Node n);
		void VisitNil(NilNode n);
		void VisitLiteral(LiteralNode n);
		void VisitSymbol(SymbolNode n);
		void VisitAddSub(AddSubNode n);
		void VisitMulDiv(MulDivNode n);
		void VisitPow(PowNode n);
		void VisitNegate(NegateNode n);
		void VisitFx(FxNode n);
		void VisitRelational(RelationalNode n);
		void VisitLogic(LogicNode n);
		void VisitLogicNot(LogicNotNode n);
	}

	public class NodeVisitorImpl : NodeVisitor
	{
		public virtual void VisitNode(Node n) {}
		public virtual void VisitNil(NilNode n) {}
		public virtual void VisitLiteral(LiteralNode n) {}
		public virtual void VisitSymbol(SymbolNode n) {}
		public virtual void VisitAddSub(AddSubNode n) {}
		public virtual void VisitMulDiv(MulDivNode n) {}
		public virtual void VisitPow(PowNode n) {}
		public virtual void VisitNegate(NegateNode n) {}
		public virtual void VisitFx(FxNode n) {}	
		public virtual void VisitRelational(RelationalNode n) {}
		public virtual void VisitLogic(LogicNode n) {}
		public virtual void VisitLogicNot(LogicNotNode n) {}
		
		public void ChildAccept(Node n)
		{
			int N = n.ChildCount;
			for(int i = 0; i < N; i++)
				n.Accept(this);
		}
	}

	/// <summary>
	/// The base class!
	/// </summary>
	public abstract class Node : ICloneable
	{	
		public abstract object Clone();

		public virtual int ChildCount { get { return 0;}}
		public virtual Node this[int i] { get { return null; } set {} }
		public virtual void Accept(NodeVisitor nv) { nv.VisitNode(this); }


		public static implicit operator Node(float f) 
		{
			return new LiteralNode(f);
		}

		public static explicit operator Node(string n)
		{
			return new SymbolNode(n);
		}
		
		public static Node operator +(Node c1, Node c2) 
		{
			return (new AddSubNode(c1, c2, true));
		}

		public static Node operator -(Node c1, Node c2) 
		{
			return (new AddSubNode(c1, c2, false));
		}

		public static Node operator *(Node c1, Node c2) 
		{
			return (new MulDivNode(c1, c2, true));
		}

		public static Node operator /(Node c1, Node c2) 
		{
			return (new MulDivNode(c1, c2, false));
		}

		public static Node operator ^(Node c1, Node c2) 
		{
			return (new PowNode(c1, c2));
		}

		public static Node operator -(Node c1) 
		{
			return (new NegateNode(c1));
		}
		
		public static Node Cos(Node c)
		{
			return new FxNode(FxNode.FxType.Cos, c);
		}

		public static Node Sin(Node c)
		{
			return new FxNode(FxNode.FxType.Sin, c);
		}
		
		public static Node Log(Node c)
		{
			return new FxNode(FxNode.FxType.Ln, c);
		}
	}

	/// <summary>
	/// A Constant Value
	/// </summary>
	public class LiteralNode : Node
	{
		private float literal;

		public float Value
		{
			get 
			{
				return literal;
			}
			set 
			{
				literal = value;
			}
		}

		public LiteralNode(float v)
		{
			Value = v;
		}

		public override object Clone()
		{
			return new LiteralNode(Value);
		}

		public override string ToString()
		{
			return ""+Value;
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitLiteral(this);
		}

		public static LiteralNode One = new LiteralNode(1);
		public static LiteralNode Zero = new LiteralNode(0);

	}

	/// <summary>
	/// The Null Node
	/// </summary>
	public class NilNode : Node
	{
		public static NilNode Nil = new NilNode();

		public override object Clone()
		{
			return this;
		}

		private NilNode()
		{
		
		}

		public override string ToString()
		{
			return "NILNODE";
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitNil(this);
		}

	}

	/// <summary>
	/// A systmbol
	/// </summary>
	public class SymbolNode : Node
	{
		private string sym;

		public string Symbol
		{
			get 
			{
				return sym;
			}
			set 
			{
				sym = value;
			}
		}

		public SymbolNode(string s)
		{
			Symbol = s;
		}

		public override object Clone()
		{
			return new SymbolNode(Symbol);
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitSymbol(this);
		}

		public override string ToString()
		{
			return Symbol;
		}

	}

	/// <summary>
	/// A Node with only one child
	/// </summary>
	public abstract class UnNode : Node
	{
		Node child;

		public UnNode(Node n)
		{
			child = n;
		}

		public UnNode()
		{
			child = NilNode.Nil;
		}

		public override int ChildCount 
		{
			get { return 1; }
		}

		public override Node this[int idx]
		{
			get 
			{
				return child;
			}
			set 
			{
				child = value;
			}
		}	

	}

	/// <summary>
	/// A Node with two children
	/// </summary>
	public abstract class BiNode : Node
	{
		Node left, right;

		public BiNode(Node l, Node r)
		{
			left = l;
			right = r;
		}

		public BiNode()
		{
			left = right = NilNode.Nil;
		}

		public Node Left
		{
			get { return left; } 
			set { left = value; }
		}

		public static void Swap(ref Node a, ref Node b)
		{
			Node c = a;
			a = b;
			b = c;
		}

		public Node Right
		{
			get { return right; } 
			set { right = value; }
		}

		public override int ChildCount 
		{
			get { return 2; }
		}

		public override Node this[int idx]
		{
			get 
			{
				return idx == 0 ? Left : Right;
			}
			set 
			{
				if(idx == 0) Left = value; else Right = value;
			}
		}	
	}



}
