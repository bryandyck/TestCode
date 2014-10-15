// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Specialized;

namespace MathExpr
{

	public class AddSubNode : BiNode
	{
		bool addop;

		public AddSubNode(Node l, Node r, bool isadd)
		{
			IsAddition = isadd;
			if(isadd && r is LiteralNode && !(l is LiteralNode)) 
				Swap(ref l,ref r);
			Left = l;
			Right = r;
		}

		public bool IsAddition 
		{
			get { return addop; }
			set { addop = value; }
		}

		public override object Clone()
		{
			return new AddSubNode((Node)Left.Clone(), (Node)Right.Clone(),IsAddition);
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitAddSub(this);
		}

		public override string ToString()
		{
			return "(" + Left.ToString() + ")" + (IsAddition ? "+" : "-") + "(" + Right.ToString() + ")";
		}

	}

	public class MulDivNode : BiNode
	{
		bool mulop;

		public MulDivNode(Node l, Node r, bool ismul)
		{
			IsMultiplication = ismul;
			// try to reorder
			if(ismul && r is LiteralNode && !(l is LiteralNode)) 
				Swap(ref l,ref r);
			Left = l;
			Right = r;
		}

		public bool IsMultiplication
		{
			get { return mulop; }
			set { mulop = value; }
		}

		public override object Clone()
		{
			return new MulDivNode((Node)Left.Clone(), (Node)Right.Clone(),IsMultiplication);
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitMulDiv(this);
		}

		public override string ToString()
		{
			return "(" + Left.ToString() + ")" + (IsMultiplication ? "*" : "/") + "(" + Right.ToString() + ")";
		}
		
	}
	
	/// <summary>
	/// Relational Operator &lt; &gt; == !=
	/// </summary>
	public class RelationalNode : BiNode
	{
		public enum Operation { Lt, Gt, Eq, Neq, Lte, Gte };

		public RelationalNode(Node l, Node r, Operation op) : base(l,r)
		{			
			nodeOp = op;
		}

		public override object Clone()
		{
			return new RelationalNode((Node)Left.Clone(), (Node)Right.Clone(), Op);
		}

		public override string ToString()
		{
			string opstr;
			switch(Op)
			{
				case Operation.Eq: opstr = "="; break;
				case Operation.Gt: opstr = "<"; break;
				case Operation.Lt: opstr = ">"; break;
				case Operation.Gte: opstr = ">="; break;
				case Operation.Lte: opstr = "<="; break;
				case Operation.Neq: opstr = "!="; break;
				default:
					opstr = "";
					break;
			}
			return "(" + Left.ToString() + ")" + opstr + "(" + Right.ToString() + ")";
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitRelational(this);
		}

		public Operation Op
		{
			get { return nodeOp; }
			set { nodeOp = value; }
		}

		Operation nodeOp;
	}
	
	/// <summary>
	/// A Logical Operator: And Or ...
	/// </summary>
	public class LogicNode : BiNode
	{
		public enum Operation { And, Or };

		public LogicNode(Node l, Node r, Operation op) : base(l,r)
		{			
			nodeOp = op;
		}

		public override object Clone()
		{
			return new LogicNode((Node)Left.Clone(), (Node)Right.Clone(), Op);
		}

		public override string ToString()
		{
			string opstr;
			switch(Op)
			{
				case Operation.And: opstr = "&&"; break;
				case Operation.Or: opstr = "||"; break;
				default:
					opstr = "";
					break;
			}

			return "(" + Left.ToString() + ")" + opstr + "(" + Right.ToString() + ")";
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitLogic(this);
		}

		public Operation Op
		{
			get { return nodeOp; }
			set { nodeOp = value; }
		}

		Operation nodeOp;

	}

	/// <summary>
	/// Exponentiation
	/// </summary>
	public class PowNode : BiNode
	{			
		public PowNode(Node l, Node r)
		{
			Left = l;
			Right = r;
		}

		public override object Clone()
		{
			return new PowNode((Node)Left.Clone(), (Node)Right.Clone());
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitPow(this);
		}

		public override string ToString()
		{
			return "(" + Left.ToString() + ")^(" + Right.ToString() + ")";
		}
	}

	/// <summary>
	/// The Logical Negation
	/// </summary>
	public class LogicNotNode : UnNode
	{
		public LogicNotNode(Node c) : base(c)
		{		
		}

		public override object Clone()
		{
			return new LogicNotNode((Node)this[0].Clone());
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitLogicNot(this);
		}

		public override string ToString()
		{
			return "!("+this[0]+")";
		}
	}
	
	public class NegateNode : UnNode
	{
		public NegateNode(Node c)
		{
			this[0] = c;
		}

		public override object Clone()
		{
			return new NegateNode(this[0]);
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitNegate(this);
		}

		public override string ToString()
		{
			return "-" + "(" + this[0].ToString() + ")";
		}
	}

	// trigonometric node
	public class FxNode : UnNode
	{
		public enum FxType { Null, Cos, Sin, Ln, _Custom};
		FxType fx;
		string name;

		public FxNode(string name, Node child)
		{
			// name to fx also as uncased
			try 
			{
				Fx = (FxType)Enum.Parse(typeof(FxType), name, true);
			}
			catch (ArgumentException )
			{
				Fx = FxType._Custom;
				this.name = name;
			}
			this[0] = child;
		}

		public FxNode(FxType f, Node child)
		{
			Fx = f;
			this[0] = child;
		}

		public FxType Fx
		{
			get { return fx;}
			set { fx = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public override object Clone()
		{
			return new FxNode(Name, this[0]);
		}

		public override void Accept(NodeVisitor nv) 
		{
			nv.VisitFx(this);
		}

		public override string ToString()
		{
			return Enum.GetName(typeof(FxType), Fx) + "(" + this[0].ToString() + ")";
		}		
	}

}
