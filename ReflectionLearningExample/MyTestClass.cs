using System;

namespace ReflectionLearningExample
{
	/// <summary>
	/// Summary description for MyTestClass.
	/// </summary>
	[MyClassSetting1(true)]
	[MyClassSetting2(1234)]
	public class MyTestClass
	{

		private int _PrivateVar1;
		public bool _PrivateVar2;
		public MyTestClass _PrivateVar3;
		protected Form _PrivateVar4;

		public MyTestClass()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void MyTestMethod1(string p1, string p2, int p3, bool p4, Form p5)
		{
		}

		public string MyTestMethod2(bool p1, DateTime p2)
		{
			return null;
		}

		public string MyTestMethod3()
		{
			return Guid.NewGuid().ToString();
		}

		public void MyTestMethod4()
		{
			System.Windows.Forms.MessageBox.Show("Test");
		}

		public bool Prop1 { get { return false; } set {} }
		public DateTime Prop2 { get { return DateTime.Now; } set {} }
		public string Prop3 { get { return Prop3; } set {} }
		public int Prop4 { get { return this.GetHashCode(); } set {} }
	}
}
