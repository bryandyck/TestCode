// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;
using MathExpr;

	class Test
	{
		static Compiler nc = new Compiler();
		static Evaluator e = new Evaluator();
		static Derivator deriv = new Derivator();
		static Parser par = new Parser();

		static void Main(string[] args)
		{
			// test expressions
			Functor f;
			
			// Basic Expression Parsing and Constant Folding
			Console.WriteLine("**** Basic Expression Parsing and Constant Folding");
			// 1+3
			//new AddSubNode(new LiteralNode(3), new LiteralNode(4), true);											
			Node n = par.Parse("3+4");
			Console.Out.WriteLine("Expression " + n);
			Console.Out.WriteLine("Basic Evaluation: " + e.Evaluate(n));
			Node nd = deriv.Derive(n, "x");
			Console.Out.WriteLine("Derivation of " + n + " is " + nd);
			ConstantFolding cf = new ConstantFolding();
			n = cf.Fold(n);
			Console.Out.WriteLine("Folded is " + n);

			// Evaluation with Value assigned toSymbol 
			// x+4
			// new AddSubNode(new SymbolNode("x"), new LiteralNode(4), true);				
			Console.WriteLine("**** Evaluation with Value assigned toSymbol ");
			n = par.Parse("x+4");
			e["x"] = 100;
			Console.Out.WriteLine("Basic Evaluation with Symbol: " + e.Evaluate(n));
			nd = deriv.Derive(n, "x");
			Console.Out.WriteLine("Derivation of " + n + " is " + nd);
			Console.Out.WriteLine("Derivation Evaluation" + e.Evaluate(nd));


			// Expression Derivation
			// n = new MulDivNode(new SymbolNode("x"), new LiteralNode(4),true);				
			Console.WriteLine("**** Expression Derivation");
			n = par.Parse("x*4");
			nd = deriv.Derive(n, "x");
			Console.Out.WriteLine("Derivation of " + n + " is " + nd);
			
			// Expression Compilation

			// n = new MulDivNode(new MulDivNode(new SymbolNode("x"), new LiteralNode(4),true),  new SymbolNode("x"), true);				
			Console.WriteLine("**** Expression Compilation");
			n = par.Parse("x*4*x");
			nd = deriv.Derive(n, "x");
			e["x"] = 100;
			Console.Out.WriteLine("Derivation of " + n + " is " + nd);
			Console.Out.WriteLine("Eval of expression " + e.Evaluate(n));
			f = nc.Compile(n);
			Console.Out.WriteLine("Eval of expression using compiled " + f.evaluate(2));

			// Expression Compilation with Function Builtin Evaluation 

			// new MulDivNode(new FxNode("cos", new SymbolNode("x")), new LiteralNode(4),true);			
			Console.WriteLine("**** Expression Compilation with Builtin Functions");
			n = par.Parse("Cos(x)*4");
			nd = deriv.Derive(n, "x");
			e["x"] = (float) (2*Math.PI);
			Console.Out.WriteLine("Derivation of " + n + " is " + nd);
			Console.Out.WriteLine("Eval of fx not compiled is " + e.Evaluate(n));
			f = nc.Compile(n);
			Console.Out.WriteLine("Eval of fx compiled is " + f.evaluate((float)(2*Math.PI)));

			Console.WriteLine("**** Parse of Printed of Parsed Expressio");
			string text = "-Cos(x)*4";
			Console.Out.WriteLine("Orig: " + text + " -> " + par.Parse(text));

			// Symbol Replacement

			Console.WriteLine("**** Symbol Replacement");

			// String
			n = SymbolReplacer.ReplaceSymbol("x", par.Parse("10+y*2"), n);
			Console.WriteLine("Replaced " + n);

			// InCode
			n = SymbolReplacer.ReplaceSymbol("x", 10 + (Node)"y"*2, n);
			Console.WriteLine("Replaced " + n);

			// an example of meta expression
			Console.WriteLine("**** Meta Expressions Test");
			n = (10 + (Node)"x") * 100;
			Console.WriteLine("Expression " + n);

			// now testing code quality
			Console.WriteLine("**** Speed Testing ...");
			int N = 1000000;
			Go();
			for(int i = 0; i < N; i++)
				f.evaluate(800);
			Stop();

			// Sphere-Ray: find the intersection point of a sphere with a ray using Newton-Raphson
			Console.WriteLine("**** Sphere-Ray Intersection using Newton-Raphson");

			TestSphereString();
			TestSphereSym();

			Console.ReadLine();
		}

		// Sphere-Ray Intersection Using a String
		static void TestSphereString()
		{
			Functor f, fd;

			Node sphere = par.Parse("x^2+y^2-100");
			Node [] line = { par.Parse("2+3*t"), par.Parse("4+5*t")};
			Node sphereline = SymbolReplacer.ReplaceSymbol("y", line[1], SymbolReplacer.ReplaceSymbol("x", line[0], sphere));
			Node spherelinederiv = deriv.Derive(sphereline, "t");
			
			Console.Out.WriteLine("The Sphere " + sphere);
			Console.Out.WriteLine("The Sphere " + line[0] + " & " + line[1]);
			Console.Out.WriteLine("The Sphere " + sphereline);
			Console.Out.WriteLine("The Sphere " + spherelinederiv);

			// find the root
			f = nc.Compile(sphereline);
			fd = nc.Compile(spherelinederiv);
			float r = NewtonRaphson.FindRoot(5, new NewtonRaphson.Fx1(f.evaluate), new NewtonRaphson.Fx1(fd.evaluate), 10000);
			Console.Out.WriteLine("Solution is " + r + " and value is " + f.evaluate(r) + " and deriv " + fd.evaluate(r));		
		}

		// Sphere-Ray Intersection Using a In-Code Node
		static void TestSphereSym()
		{
			Functor f, fd;

			Node sphere = ((Node)("x") ^ 2) + ((Node)("y") ^ 2) - 100;
			Node [] line = { 2+ 3 * (Node)"t", 4 + 5 * (Node)"t"};
			Node sphereline = SymbolReplacer.ReplaceSymbol("y", line[1], SymbolReplacer.ReplaceSymbol("x", line[0], sphere));
			Node spherelinederiv = deriv.Derive(sphereline, "t");
			
			Console.Out.WriteLine("The Sphere " + sphere);
			Console.Out.WriteLine("The Sphere " + line[0] + " & " + line[1]);
			Console.Out.WriteLine("The Sphere " + sphereline);
			Console.Out.WriteLine("The Sphere " + spherelinederiv);

			// find the root
			f = nc.Compile(sphereline);
			fd = nc.Compile(spherelinederiv);
			float r = NewtonRaphson.FindRoot(5, new NewtonRaphson.Fx1(f.evaluate), new NewtonRaphson.Fx1(fd.evaluate), 10000);
			Console.Out.WriteLine("Solution is " + r + " and value is " + f.evaluate(r) + " and deriv " + fd.evaluate(r));		
		}


		static void Go()
		{
			start = DateTime.Now.Ticks;
		}

		static void Stop()
		{
			long stop = DateTime.Now.Ticks;
			Console.Out.WriteLine(stop-start);
		}
	
		static long start;
	}

