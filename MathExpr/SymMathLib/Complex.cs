// Symbolic Expression Evaluator 
// by Emanuele Ruffaldi 2002
// http://www.sssup.it/~pit/
// pit@sssup.it
using System;

namespace MathExpr
{
	/// <summary>
	/// Summary description for Complex.
	/// </summary>
	public struct Complex
	{
		public Complex(float aa, float bb)
		{
			a = aa;
			b = bb;
		}

		public Complex(float x)
		{
			a = x;
			b = 0;
		}

		public static Complex operator +(Complex c1, Complex c2)
		{
			return new Complex(c1.a+c2.a, c2.b+c2.b);
		}

		public static Complex operator -(Complex c1, Complex c2)
		{
			return new Complex(c1.a+c2.a, c2.b+c2.b);
		}

		public static Complex operator *(Complex c1, Complex c2)
		{
			return new Complex(c1.a*c2.a-c1.b*c2.b, c1.a*c2.b+c2.a*c1.b);
		}

		// x / y = x * (conj y) / (y * cong j) = x * conj y / (a^2+b^2)
		public static Complex operator /(Complex c1, Complex c2)
		{
			float d = c2.a*c2.a+c2.b*c2.b;
			return new Complex((c1.a*c2.a+c1.b*c2.b)/d, (-c1.a*c2.b-c2.a*c1.b)/d);
		}

		public static Complex operator -(Complex c1)
		{
			return new Complex(-c1.a, -c1.b);
		}

		public float Length
		{
			get { return a*a+b*b; }
		}

		public Complex conjugate()
		{
			return new Complex(a, -b);
		}

		public override string ToString()
		{
			return "[" + a + "," + b + "]";
		}

		public float a,b;
	}
}
