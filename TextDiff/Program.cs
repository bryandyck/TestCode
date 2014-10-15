using System;

namespace BizKit.TextDiff
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Please enter path to file 1: ");
            //string fileName1 = Console.ReadLine();
            //Console.WriteLine("Please enter path to file 2: ");
            //string fileName2 = Console.ReadLine();

            string oldFile = @"
Bryan
Layla
Charlotte
Jie
Henry
";
            string newFile = @"
Layla
Charlotte
Aadi
Srikanth
Jie
Henrita
Joe";


            TextDiffer diff = new TextDiffer();
            diff.Execute(oldFile, newFile);
            string mergedText = "";
        }
    }
}
