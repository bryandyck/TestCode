using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRRFuncTest
{

    internal static class Irr
{
    // Methods
    internal static bool _validCfs@26(FSharpList<double> cfs, bool pos, bool neg)
    {
        FSharpList<double> list2;
        double num;
    Label_0000:
        if (pos && neg)
        {
            return true;
        }
        if (cfs.get_TailOrNull() != null)
        {
            FSharpList<double> list = cfs;
            if (list.get_HeadOrDefault() > 0.0)
            {
                list2 = list.get_TailOrNull();
                num = list.get_HeadOrDefault();
                neg = neg;
                pos = true;
                cfs = list2;
                goto Label_0000;
            }
        }
        if (cfs.get_TailOrNull() != null)
        {
            list2 = cfs;
            if (list2.get_HeadOrDefault() <= 0.0)
            {
                FSharpList<double> list3 = list2.get_TailOrNull();
                num = list2.get_HeadOrDefault();
                neg = true;
                pos = pos;
                cfs = list3;
                goto Label_0000;
            }
        }
        if (cfs.get_TailOrNull() != null)
        {
            throw new Exception("Should never get here");
        }
        return false;
    }

    [CompilationArgumentCounts(new int[] { 1, 1 })]
    internal static double calcIrr(IEnumerable<double> cfs, double guess)
    {
        Common.elseThrow("There must be one positive and one negative cash flow", validCfs(cfs));
        return irr(cfs, guess);
    }

    [CompilationArgumentCounts(new int[] { 1, 1, 1 })]
    internal static double calcMirr(IEnumerable<double> cfs, double financeRate, double reinvestRate)
    {
        Common.elseThrow("financeRate cannot be -100%", financeRate != -1.0);
        Common.elseThrow("reinvestRate cannot be -100%", reinvestRate != -1.0);
        Common.elseThrow("cfs must contain more than one cashflow", SeqModule.Length<double>(cfs) != 1);
        Common.elseThrow("The NPV calculated using financeRate and the negative cashflows in cfs must be different from zero", npv(financeRate, SeqModule.Map<double, double>(new calcMirr@44(), cfs)) != 0.0);
        return mirr(cfs, financeRate, reinvestRate);
    }

    [CompilationArgumentCounts(new int[] { 1, 1 })]
    internal static double calcNpv(double r, IEnumerable<double> cfs)
    {
        Common.elseThrow("r cannot be -100%", r != -1.0);
        return npv(r, cfs);
    }

    [CompilationArgumentCounts(new int[] { 1, 1, 1 })]
    internal static double calcXirr(IEnumerable<double> cfs, IEnumerable<DateTime> dates, double guess)
    {
        Common.elseThrow("There must be one positive and one negative cash flow", validCfs(cfs));
        Common.elseThrow("In dates, one date is less than the first date", !SeqModule.Exists<DateTime>(new calcXirr@53(dates), dates));
        Common.elseThrow("cfs and dates must have the same length", SeqModule.Length<double>(cfs) == SeqModule.Length<DateTime>(dates));
        return xirr(cfs, dates, guess);
    }

    [CompilationArgumentCounts(new int[] { 1, 1, 1 })]
    internal static double calcXnpv(double r, IEnumerable<double> cfs, IEnumerable<DateTime> dates)
    {
        Common.elseThrow("r cannot be -100%", r != -1.0);
        Common.elseThrow("In dates, one date is less than the first date", !SeqModule.Exists<DateTime>(new calcXnpv@48(dates), dates));
        Common.elseThrow("cfs and dates must have the same length", SeqModule.Length<double>(cfs) == SeqModule.Length<DateTime>(dates));
        return xnpv(r, cfs, dates);
    }

    [CompilationArgumentCounts(new int[] { 1, 1 })]
    internal static double irr(IEnumerable<double> cfs, double guess)
    {
        return Common.findRoot(new irr@12(cfs), guess);
    }

    [CompilationArgumentCounts(new int[] { 1, 1, 1 })]
    internal static double mirr(IEnumerable<double> cfs, double financeRate, double reinvestRate)
    {
        double y = SeqModule.Length<double>(cfs);
        IEnumerable<double> enumerable = SeqModule.Map<double, double>(new positives@15(), cfs);
        IEnumerable<double> enumerable2 = SeqModule.Map<double, double>(new negatives@16(), cfs);
        return (Math.Pow((-npv(reinvestRate, enumerable) * Math.Pow(1.0 + reinvestRate, y)) / (npv(financeRate, enumerable2) * (1.0 + financeRate)), 1.0 / (y - 1.0)) - 1.0);
    }

    [CompilationArgumentCounts(new int[] { 1, 1 })]
    internal static double npv(double r, IEnumerable<double> cfs)
    {
        using (IEnumerator<double> enumerator = SeqModule.MapIndexed<double, double>(new npv@11(r), cfs).GetEnumerator())
        {
            double num2 = 0.0;
            while (true)
            {
                if (!enumerator.MoveNext())
                {
                    break;
                }
                num2 += enumerator.Current;
            }
            return num2;
        }
    }

    internal static bool validCfs(IEnumerable<double> cfs)
    {
        return _validCfs@26(SeqModule.ToList<double>(cfs), false, false);
    }

    [CompilationArgumentCounts(new int[] { 1, 1, 1 })]
    internal static double xirr(IEnumerable<double> cfs, IEnumerable<DateTime> dates, double guess)
    {
        return Common.findRoot(new xirr@22(cfs, dates), guess);
    }

    [CompilationArgumentCounts(new int[] { 1, 1, 1 })]
    internal static double xnpv(double r, IEnumerable<double> cfs, IEnumerable<DateTime> dates)
    {
        DateTime time = SeqModule.Head<DateTime>(dates);
        using (IEnumerator<double> enumerator = SeqModule.Map2<DateTime, double, double>(new xnpv@21(r, time), dates, cfs).GetEnumerator())
        {
            double num2 = 0.0;
            while (true)
            {
                if (!enumerator.MoveNext())
                {
                    break;
                }
                num2 += enumerator.Current;
            }
            return num2;
        }
    }

    // Nested Types
    [Serializable]
    internal class calcMirr@44 : FSharpFunc<double, double>
    {
        // Methods
        internal calcMirr@44()
        {
        }

        public override double Invoke(double cf)
        {
            if (cf < 0.0)
            {
                return cf;
            }
            return 0.0;
        }
    }

    internal class calcXirr@53 : FSharpFunc<DateTime, bool>
    {
        // Fields
        public IEnumerable<DateTime> dates;

        // Methods
        internal calcXirr@53(IEnumerable<DateTime> dates)
        {
            this.dates = dates;
        }

        public override bool Invoke(DateTime x)
        {
            return LanguagePrimitives.HashCompare.GenericLessThanIntrinsic<DateTime>(x, SeqModule.Head<DateTime>(this.dates));
        }
    }

    internal class calcXnpv@48 : FSharpFunc<DateTime, bool>
    {
        // Fields
        public IEnumerable<DateTime> dates;

        // Methods
        internal calcXnpv@48(IEnumerable<DateTime> dates)
        {
            this.dates = dates;
        }

        public override bool Invoke(DateTime x)
        {
            return LanguagePrimitives.HashCompare.GenericLessThanIntrinsic<DateTime>(x, SeqModule.Head<DateTime>(this.dates));
        }
    }

    internal class irr@12 : FSharpFunc<double, double>
    {
        // Fields
        public IEnumerable<double> cfs;

        // Methods
        internal irr@12(IEnumerable<double> cfs)
        {
            this.cfs = cfs;
        }

        public override double Invoke(double r)
        {
            return Irr.npv(r, this.cfs);
        }
    }

    [Serializable]
    internal class negatives@16 : FSharpFunc<double, double>
    {
        // Methods
        internal negatives@16()
        {
        }

        public override double Invoke(double cf)
        {
            if (cf < 0.0)
            {
                return cf;
            }
            return 0.0;
        }
    }

    [Serializable]
    internal class npv@11 : OptimizedClosures.FSharpFunc<int, double, double>
    {
        // Fields
        public double r;

        // Methods
        internal npv@11(double r)
        {
            this.r = r;
        }

        public override double Invoke(int i, double cf)
        {
            double y = i + 1;
            return (cf * (1.0 / Math.Pow(1.0 + this.r, y)));
        }
    }

    [Serializable]
    internal class positives@15 : FSharpFunc<double, double>
    {
        // Methods
        internal positives@15()
        {
        }

        public override double Invoke(double cf)
        {
            if (cf > 0.0)
            {
                return cf;
            }
            return 0.0;
        }
    }

    internal class xirr@22 : FSharpFunc<double, double>
    {
        // Fields
        public IEnumerable<double> cfs;
        public IEnumerable<DateTime> dates;

        // Methods
        internal xirr@22(IEnumerable<double> cfs, IEnumerable<DateTime> dates)
        {
            this.cfs = cfs;
            this.dates = dates;
        }

        public override double Invoke(double r)
        {
            return Irr.xnpv(r, this.cfs, this.dates);
        }
    }

    [Serializable]
    internal class xnpv@21 : OptimizedClosures.FSharpFunc<DateTime, double, double>
    {
        // Fields
        public DateTime d0;
        public double r;

        // Methods
        internal xnpv@21(double r, DateTime d0)
        {
            this.r = r;
            this.d0 = d0;
        }

        public override double Invoke(DateTime d, double cf)
        {
            TimeSpan span = (TimeSpan) (d - this.d0);
            return (cf / Math.Pow(1.0 + this.r, ((double) span.Days) / 365.0));
        }
    }
}

 
}
