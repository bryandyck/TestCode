// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;
using System.Text.RegularExpressions;

namespace MathExpr
{
	/// <summary>
	/// A Descendent recursive parser
	/// </summary>
	public class Parser
	{
		protected enum Token { Literal, Symbol, Operator , Eof};
		protected enum Levels { Sum, Mul, }		

		/// <summary>
		/// Initialize the Parser, it prepares Compiled Regular Expressions to Tokinize
		/// </summary>
		public Parser()
		{
			reOps = new Regex(@"^\s*(&&|\|\||<=|>=|==|!=|[=+\-*/^()!<>])", RegexOptions.Compiled);
			reSym = new Regex(@"^\s*(\-?\b*[_a-zA-Z]+[_a-zA-Z0-9]*)",RegexOptions.Compiled);
			reLit = new Regex(@"^\s*([0-9]+(\.[0-9]+)?)",RegexOptions.Compiled);
		}

		/// <summary>
		/// Utility to print all the token of a string
		/// </summary>
		/// <param name="s"></param>
		public void PrintTokens(string s)
		{
			Token t;
			while((t = NextToken()) != Token.Eof) 
			{			
				switch(t)
				{
					case Token.Literal: Console.WriteLine("Lit " + literal); break;
					case Token.Symbol:  Console.WriteLine("Sym " + symbol); break;
					case Token.Operator: Console.WriteLine("Op " + op); break;
					default:
						break;
				}
				Console.WriteLine("Remaining is: " + text); 
			}		
		}

		/// <summary>
		/// Parses an expression and returns a Node tree
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public Node Parse(string s)
		{
			text = s;									
			NextToken();
			return topExpression();
		}

		// right associative
		protected Node topExpression()
		{
			return logExpression();			
		}

		protected Node logExpression()
		{
			Node n = relExpression();
			while (current == Token.Operator && (op == "&&"||op=="||")) 
			{
				string ops = op;
				NextToken();
				Node rn = relExpression();
				n = new LogicNode(n,rn, ops == "&&" ? LogicNode.Operation.And : LogicNode.Operation.Or);
			}
			return n;		
		}

		protected Node relExpression()
		{
			Node n = addExpression();
			while (current == Token.Operator)
			{
				RelationalNode.Operation ops;
				switch(op)
				{
					case "<": ops = RelationalNode.Operation.Lt; break;
					case ">": ops = RelationalNode.Operation.Gt; break;
					case "<=": ops = RelationalNode.Operation.Lte; break;
					case ">=": ops = RelationalNode.Operation.Gte; break;
					case "!=": ops = RelationalNode.Operation.Neq; break;
					case "=":
					case "==":
						ops = RelationalNode.Operation.Eq; break;
					default:
						return n;
				}
				NextToken();
				Node rn = addExpression();
				n = new RelationalNode(n,rn, ops);
			}
			return n;		
		}

		protected Node addExpression()
		{
			Node n = mulExpression();
			while (current == Token.Operator && (op == "+"||op=="-")) 
			{
				string ops = op;
				NextToken();
				Node rn = mulExpression();
				n = new AddSubNode(n,rn, ops == "+");
			}
			return n;
		}

		protected Node mulExpression()
		{
			Node n = postfixExpression();
			while (current == Token.Operator && (op == "*"||op=="/")) 
			{
				string ops = op;
				NextToken();
				Node rn = postfixExpression();
				n = new MulDivNode(n,rn, ops == "*");
			}
			return n;
		}

		protected Node postfixExpression()
		{
			Node n = prefixExpression();
			while (n != null && current == Token.Operator && op == "^") 
			{
				NextToken();
				Node rn = prefixExpression();
				n = new PowNode(n,rn);
			}
			return n;
		}

		protected Node prefixExpression()
		{
			if(current == Token.Operator && (op == "-") )
			{
				NextToken();
				return new NegateNode(primaryExpression());
			}
			else if(current == Token.Operator && (op == "!") )
			{
				NextToken();
				return new LogicNotNode(primaryExpression());			
			}
			else
				return primaryExpression();
		}

		protected Node primaryExpression()
		{
			string fx;
			switch(current)
			{
				case Token.Symbol:
					NextToken();
					fx = symbol;
					if(current == Token.Operator && op == "(") {
						// function
						NextToken();						
						Node n = topExpression();
						if(current == Token.Operator && op == ")") {
							NextToken();
						}
						return new FxNode(fx, n);
					}
					else
						return new SymbolNode(symbol);
				case Token.Literal:
					NextToken();
					return new LiteralNode(literal);
				case Token.Operator:
					break;
				case Token.Eof:
					return null;
				default:
					// exception
					return null;
			}

			switch(op)
			{
				case "(":
					NextToken();
					Node n = topExpression();
					if(current != Token.Operator && op != ")") 
					{
						// exception
						return null;
					}
					else 
					{
						NextToken();						
						return n;
					}
				case ")":
					return null;
				default:
					// exception
					return null;
			}
		}

		// token extractor, really short using Regular Expressions
		protected Token NextToken()
		{
			Match m;
			
			m = reOps.Match(text);
			if(m.Length != 0) 
			{
				op = m.Groups[1].Value;
				start += m.Length;
				text = text.Substring(m.Length);
				current = Token.Operator;				
			}
			else 
			{
				m = reSym.Match(text);
				if(m.Length != 0) 
				{
					symbol = m.Groups[1].Value;
					start += m.Length;
					text = text.Substring(m.Length);
					current = Token.Symbol;
				}
				else 
				{
					m = reLit.Match(text);
					if(m.Length != 0) 
					{
						literal = (float)Double.Parse(m.Groups[1].Value);
						start += m.Length;
						text = text.Substring(m.Length);
						current = Token.Literal;
					}
					else 
						current = Token.Eof;
				}
			}
			return current;
		}

		Token  current;		// current token
		string symbol;		// current symbol
		float  literal;		// current literal
		string op;			// current operator
		int start;			// substring start position
		string text;		// text to be searched
		Regex  reOps;		// regular expression for Ops
		Regex  reLit;		// regular expression for Lits
		Regex  reSym;		// regular expression for Syms
	}
}
