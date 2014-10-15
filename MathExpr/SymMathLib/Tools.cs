// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;
using System.Collections.Specialized;

namespace MathExpr
{
	// Symbol Storage Class
	public class NamedObjectCollection : NameObjectCollectionBase
	{
		public object this[string s]
		{
			get { return BaseGet(s);}
			set { BaseSet(s, value); }
		}

		public void Clear()
		{
			BaseClear();
		}
	};

	public class SymbolReplacer
	{		
		private string symbol;
		private Node   target;

		public SymbolReplacer(string name, Node dst)
		{
			symbol = name;
			target = dst;
		}

		private Node replace(Node src)
		{
			if(src is SymbolNode && ((SymbolNode)src).Symbol.Equals(symbol)) 
				return target;
			
			Node [] x = new Node[4];
			int n = src.ChildCount;
			bool changed = false;

			for(int i = 0; i < n; i++) 
			{
				x[i] = replace(src[i]);
				changed = changed | x[i] != src[i];				
			}

			if(changed) 
			{
				Node r = (Node)src.Clone();		// ugly
				for(int i = 0; i < n; i++) 				
					r[i] = x[i];				
				//Console.WriteLine("Node changed " + src + " as " + r);
				return r;
			}
			else
				return src;
		}

		public static Node ReplaceSymbol(string name, Node dst, Node src)
		{
			SymbolReplacer sr = new SymbolReplacer(name, dst);			
			return sr.replace(src);
		}
	}


	// each function class has it's own members specially for derivation
	public class ConstantFolding
	{
		Evaluator evaluator = new Evaluator();

		public Node Fold(Node src)
		{
			bool has;
			src = DoFold(src, out has);
			if(!has) 
				return new LiteralNode(evaluator.Evaluate(src));			
			else 
				return src;
		}
		
		private Node DoFold(Node src, out bool has)
		{
			if(src is SymbolNode) 
			{
				has = true;
				return src;
			}

			// Ask to the Children
			int n = src.ChildCount;
			bool hasSymbols = false;
			// mark subexpressions that can be folded
			int bitfield = 0;
			for(int i = 0; i < n; i++) 
			{
				bool hasResult;
				src[i] = DoFold(src[i], out hasResult);
				hasSymbols |= hasResult;
				bitfield |= (1 << i);
			}

			// if not the whole expression can be folded ... 
			// fold only the children
			if(!hasSymbols)
				for(int i = 0; i < n; i++) 
				{
					if((bitfield & (1 << i)) != 0) 
						src[i] = new LiteralNode(evaluator.Evaluate(src[i]));
				}
 
			has = hasSymbols;
			return src;
		}
	}

	public delegate double CustomFX(double a);
	public delegate double CustomFX2(double a, double b);

	/// <summary>
	/// 
	/// </summary>
	public class SymbolTable 
	{
		public object GetSymbolValue(string name)
		{
			return symbols[name];
		}
		
		public void SetSymbolValue(string symbol, float value)
		{
			symbols[symbol] = value;
		}

		public void ClearSymbols()
		{
			symbols.Clear();
			delegates.Clear();
		}

		public CustomFX GetFunction(string name)
		{
			return (CustomFX)delegates[name];
		}

		public void InstallFunction(string name, CustomFX fx)
		{
			delegates[name] = fx;
		}
		
		protected NamedObjectCollection symbols = new NamedObjectCollection();		
		protected NamedObjectCollection delegates = new NamedObjectCollection();		
	}

	public class NewtonRaphson
	{
		public delegate float Fx1(float f);

		// x[i+1] = x[i] - f(x[i])/f'(x[i])
		public static float FindRoot(float xn, Fx1 f0, Fx1 f1, int maxiter)
		{
			for(int i = 0; i < maxiter; i++) 
			{
				float fdxn = f1(xn);
				if(fdxn == 0) break;
				float fxn  = f0(xn);
				float xnnext = xn - fxn/fdxn;				
				xn = xnnext;
				if(Math.Abs(fxn) < 1E-6) break;
			}
			return xn;
		}
	}

}