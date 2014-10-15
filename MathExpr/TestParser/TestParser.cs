using System;
using MathExpr;

/// <summary>
/// This Application Test the Parser
/// </summary>
class TestParser
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main(string[] args)
	{
		Parser par = new Parser();								
		Console.Out.WriteLine("Expression {0}", par.Parse("3-4-5"));
		Console.Out.WriteLine("Expression {0}", par.Parse("3-4*5"));
		Console.Out.WriteLine("Expression {0}", par.Parse("3*2-4*5*7+(9*2)"));
		Console.Out.WriteLine("Expression {0}", par.Parse("2/4/2"));
		Console.Out.WriteLine("Expression {0}", par.Parse("(2/4)/2"));
		Console.Out.WriteLine("Expression {0}", par.Parse("2/(4/2)"));		
		Console.Out.WriteLine("Expression {0}", par.Parse("2 <= x"));		
	}
}
