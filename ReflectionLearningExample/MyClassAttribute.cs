using System;

namespace ReflectionLearningExample
{
	/// <summary>
	/// Summary description for MyClassAttribute.
	/// </summary>
	public class MyClassSetting1Attribute : Attribute 
	{
		private int _SomeValue;
		
		public MyClassSetting1Attribute(int someValue)
		{
			_SomeValue = someValue;
		}

		public int SomeValue { get { return _SomeValue; } }
	}

	public class MyClassSetting2Attribute : Attribute 
	{
		private int _SomeValue;
		
		public MyClassSetting2Attribute(int someValue)
		{
			_SomeValue = someValue;
		}

		public int SomeValue { get { return _SomeValue; } }
	}

}
