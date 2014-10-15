using MathExpr;
using System;

	/// <summary>
	/// This program tests the Relational Operators support and the
	/// user defined functions
	/// </summary>
	class TestBool
	{

		static double square(double d)
		{
			return d*d;
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			SymbolTable st = new SymbolTable();
			Compiler nc = new Compiler();
			Evaluator e = new Evaluator(st);
			Parser par = new Parser();
			Functor f;
			Node n;
			
			n = par.Parse("x+3 < 8 || x > 120");
			f = nc.Compile(n);
			Console.WriteLine(f.evaluate(121));

			st.InstallFunction("square", new CustomFX(square));
			st.SetSymbolValue("x", 18);
			n = par.Parse("square(x+2) > 120");
			Console.WriteLine(e.Evaluate(n));
		}
	}
