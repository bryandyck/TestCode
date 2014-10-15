using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowsApplication3
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class NormalDiscreteDistribution
    {
        private delegate double DoubleUnaryFunction(double x);

        // Fields
        private static readonly double[] a;
        private static readonly double[] b;
        private static readonly double[] c;
        private double c_;
        private static readonly double[] d;
        private static readonly double half;
        private double mean_;
        private const string meanName_ = "mean";
        private static readonly double one;
        private static readonly double OneOverRoot2Pi;
        private double oneOverSigma_;
        private double oneOverSigmaSqr_;
        private static readonly double[] p;
        private static readonly double[] q;
        private static readonly double root32;
        private double sigma_;
        private static readonly double sixten;
        private static readonly double sqrpi;
        private static readonly double thrsh;
        private const string varianceName_ = "variance";
        private static readonly double zero;
        //private static readonly int GAMMA_MAX_ITER = 0x7d0;
        private static readonly double MACHINE_EPSILON = 1E-12;
        private double[] values_;
        private double[] weightings_;
        
        private int SampleSize
        {
            get
            {
                if (values_ == null) return 0;
                //the minimum threashold can be changed here, but 
                //if there are not 10 samples, assume 10 to keep 
                //the standard dev. reasonable
                return Math.Max(10, values_.Length); 
            }
        }

        // Methods
        static NormalDiscreteDistribution()
        {
            a = new double[] { 2.2352520354606837, 161.02823106855587, 1067.6894854603709, 18154.981253343562, 0.065682337918207448 };
            b = new double[] { 47.202581904688245, 976.09855173777669, 10260.932208618979, 45507.789335026733 };
            c = new double[] { 0.39894151208813466, 8.8831497943883768, 93.506656132177852, 597.27027639480025, 2494.5375852903726, 6848.1904505362827, 11602.65143764735, 9842.7148383839776, 1.0765576773720192E-08 };
            d = new double[] { 22.266688044328117, 235.387901782625, 1519.3775994075547, 6485.5582982667611, 18615.571640885097, 34900.952721145979, 38912.00328609327, 19685.429676859992 };
            half = 0.5;
            p = new double[] { 0.215898534057957, 0.12740116116024736, 0.022235277870649807, 0.0014216191932278934, 2.9112874951168793E-05, 0.023073441764940174 };
            one = 1;
            q = new double[] { 1.2842600961449111, 0.46823821248086511, 0.065988137868928556, 0.0037823963320275824, 7.2975155508396618E-05 };
            sixten = 1.6;
            sqrpi = 0.3989422804014327;
            thrsh = 0.66291;
            root32 = 5.656854248;
            zero = 0;
            OneOverRoot2Pi = 1 / Math.Sqrt(6.2831853071795862);
        }

        public NormalDiscreteDistribution()
            : this((double)0, (double)1)
        {
        }

        public NormalDiscreteDistribution(double mean, double var)
        {
            this.mean_ = mean;
            this.Variance = var;
        }

        public NormalDiscreteDistribution(double[] _values, double[] _weightings)
        {
            SetValues(_values, _weightings);
        }

        public void SetValues(double[] values, double[] weightings)
        {
            double mean, var, totalWeight;
            int i;

            if (values.GetUpperBound(0) != weightings.GetUpperBound(0))
                throw new ArgumentException("The number of values does not match the number of weightings. The passed arrays should be of equal length.");

            values_ = values;
            weightings_ = weightings;

            //get the mean
            mean = 0;
            totalWeight = 0;
            for (i = 0; i < values_.GetUpperBound(0) + 1; i++)
            {
                mean = mean + values_[i] * weightings_[i];
                totalWeight += weightings_[i];
            }
            //mean = mean / (values_.GetUpperBound(0) + 1);

            //get the variance
            var = 0;
            for (i = 0; i < values_.GetUpperBound(0) + 1; i++)
                var = var + Math.Pow((values_[i] - mean), 2d) * weightings_[i] * SampleSize;
            var = (var / (totalWeight * (SampleSize-1)));

            //set the values
            this.mean_ = mean;
            SetVariance(var);
        }

        #region Methods

        public double CDF(double x)
        {
            int num1;
            double num3;
            double num5;
            double num6;
            double num8;
            double num10;
            double num11;
            double num12 = (x - this.mean_) / this.sigma_;
            double num9 = double.Epsilon;
            double num4 = num12;
            double num7 = Math.Abs(num4);
            if (sigma_ == 0)
                if (x >= mean_) return 0; 
                else return 1;

            if (num7 <= thrsh)
            {
                num8 = zero;
                if (num7 > MACHINE_EPSILON)
                {
                    num8 = num4 * num4;
                }
                num6 = a[4] * num8;
                num5 = num8;
                for (num1 = 0; num1 < 3; num1++)
                {
                    num6 = (num6 + a[num1]) * num8;
                    num5 = (num5 + b[num1]) * num8;
                }
                num10 = (num4 * (num6 + a[3])) / (num5 + b[3]);
                num3 = num10;
                num10 = half + num3;
                num11 = half - num3;
            }
            else
            {
                double num2;
                if (num7 <= root32)
                {
                    num6 = c[8] * num7;
                    num5 = num7;
                    for (num1 = 0; num1 < 7; num1++)
                    {
                        num6 = (num6 + c[num1]) * num7;
                        num5 = (num5 + d[num1]) * num7;
                    }
                    num10 = (num6 + c[7]) / (num5 + d[7]);
                    num8 = Math.Floor(num7 * sixten) / sixten;
                    num2 = (num7 - num8) * (num7 + num8);
                    num10 = (Math.Exp(-((num8 * num8) * half)) * Math.Exp(-(num2 * half))) * num10;
                    num11 = one - num10;
                    if (num4 > zero)
                    {
                        num3 = num10;
                        num10 = num11;
                        num11 = num3;
                    }
                }
                else
                {
                    num10 = zero;
                    num8 = one / (num4 * num4);
                    num6 = p[5] * num8;
                    num5 = num8;
                    for (num1 = 0; num1 < 4; num1++)
                    {
                        num6 = (num6 + p[num1]) * num8;
                        num5 = (num5 + q[num1]) * num8;
                    }
                    num10 = (num8 * (num6 + p[4])) / (num5 + q[4]);
                    num10 = (sqrpi - num10) / num7;
                    num8 = Math.Floor(num4 * sixten) / sixten;
                    num2 = (num4 - num8) * (num4 + num8);
                    num10 = (Math.Exp(-((num8 * num8) * half)) * Math.Exp(-(num2 * half))) * num10;
                    num11 = one - num10;
                    if (num4 > zero)
                    {
                        num3 = num10;
                        num10 = num11;
                        num11 = num3;
                    }
                }
            }
            if (num10 < num9)
            {
                num10 = 0;
            }
            if (num11 < num9)
            {
                num11 = 0;
            }
            return num10;
        }

        public double InverseCDF(double p)
        {
            double num4;
            double num5;

            if ((p > 1) || (p < 0))
            {
                return double.NaN;
            }
            if (p == 1)
            {
                return double.PositiveInfinity;
            }
            if (p == 0)
            {
                return double.NegativeInfinity;
            }
            if (sigma_ == 0) return mean_;
            double num1 = this.mean_;
            double num2 = 1.2 * this.sigma_;
            double num3 = this.CDF(num1);
            if (num3 > p)
            {
                while (num3 > p)
                {
                    num1 -= num2;
                    num3 = this.CDF(num1);
                }
                num5 = num1 + num2;
                num4 = num1;
            }
            else
            {
                while (num3 < p)
                {
                    num1 += num2;
                    num3 = this.CDF(num1);
                }
                num5 = num1;
                num4 = num1 - num2;
            }
            return InverseCdfUsingBracket(p, num4, num5);
        }

        public double PDF(double x)
        {
            if (sigma_ == 0)
                if (x == mean_) return 1;
                else return 0;
            double num1 = x - this.mean_;
            double num2 = num1 * num1;
            return (this.c_ * Math.Exp((-0.5 * num2) * this.oneOverSigmaSqr_));
        }

        #endregion

        #region Movements

        public double Kurtosis
        {
            get
            {
                if (values_ == null) return 0;

                int i;
                double sumTo4, n, totalWeight = 0;

                n = values_.GetUpperBound(0) + 1;

                sumTo4 = 0;
                for (i = 0; i < n; i++)
                {
                    sumTo4 = sumTo4 + Math.Pow(((values_[i] - mean_) / sigma_), 4d) * weightings_[i] * SampleSize;
                    totalWeight += weightings_[i];
                }

                n = (totalWeight * SampleSize);
                return sumTo4 * (n * (n + 1) / ((n - 1) * (n - 2) * (n - 3))) - (3 * Math.Pow((n - 1), 2) / ((n - 2) * (n - 3)));
            }
        }

        public double Mean
        {
            get
            {
                return mean_;
            }
            set
            {
                mean_ = value;
                values_ = null;
            }
        }

        public double Skewness
        {
            get
            {
                if (values_ == null) return 0;

                int i;
                double sumTo3, n, totalWeight = 0;

                n = values_.GetUpperBound(0) + 1;

                sumTo3 = 0;
                for (i = 0; i < n; i++)
                {
                    sumTo3 = sumTo3 + Math.Pow(((values_[i] - mean_) / sigma_), 3) * weightings_[i] * SampleSize;
                    totalWeight += weightings_[i];
                }

                n = (totalWeight * SampleSize);
                return sumTo3 * (n / ((n - 1) * (n - 2)));
            }
        }

        public double Variance
        {
            get
            {
                return (this.sigma_ * this.sigma_);
            }
            set
            {
                if (value <= 0)
                {
                    string text1 = string.Format("Expected variance > 0 in NormalDistribution. Found variance = {0}", value);
                    throw new ArgumentException(text1);
                }

                SetVariance(value);

                values_ = null;
            }
        }

        private void SetVariance(double value)
        {
            this.sigma_ = Math.Sqrt(value);
            this.oneOverSigma_ = 1 / this.sigma_;
            this.oneOverSigmaSqr_ = this.oneOverSigma_ * this.oneOverSigma_;
            this.c_ = this.oneOverSigma_ * OneOverRoot2Pi;
        }

        #endregion

        #region Supporting Functions
        private double InverseCdfUsingBracket(double p, double lowerBound, double upperBound)
        {
            DoubleUnaryFunction function1 = new DoubleUnaryFunction(this.CDF);
            return InverseCdfUsingBracket(function1, p, lowerBound, upperBound);
        }

        private static double InverseCdfUsingBracket(DoubleUnaryFunction cdf, double p, double lowerBound, double upperBound)
        {
            double num1 = lowerBound;
            double num2 = upperBound;
            double num3 = double.MaxValue;
            double num4 = 0.5 * (num1 + num2);
            double num5 = cdf(num4);
            int num6 = 0;
            while ((Math.Abs((double)(num3 - num4)) > 1E-15) && (num6 <= 0x3e8))
            {
                if (num5 > p)
                {
                    num2 = num4;
                }
                else
                {
                    num1 = num4;
                }
                num3 = num4;
                num4 = (num2 + num1) * 0.5;
                num5 = cdf(num4);
                num6++;
            }
            if (num6 >= 0x3e8)
            {
                throw new Exception("MAX ITERATIONS EXCEEDED IN InverseCdfUsingBracket");
            }
            return num4;
        }

        private double InverseDiscreteCdfUsingBracket(double p, int lowerBound, int upperBound)
        {
            double num1 = lowerBound;
            double num2 = upperBound;
            double num3 = Math.Floor((num1 + num2) / 2);
            double num4 = double.NaN;
            double num5 = this.CDF(num3);
            int num6 = 0;
            while ((num3 != num4) && (num6 <= 0x3e8))
            {
                if (num5 > p)
                {
                    num2 = num3;
                }
                else
                {
                    num1 = num3;
                }
                num4 = num3;
                num3 = Math.Floor((num1 + num2) / 2);
                num5 = this.CDF(num3);
                num6++;
            }
            if (num6 == 0x3e8)
            {
                throw new InvalidOperationException("Failure to converge after " + 0x3e8 + " iterations in inverse CDF.");
            }
            return num3;
        }
        #endregion
    }

}

