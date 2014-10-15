using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] vals = new double[] { 10, 10, 10, 20, 20, 20, 20, 80, 80, 80 };
            double[] discreteVals = new double[] { 10, 20, 80 };
            double[] discreteWeights = new double[] { 0.3, 0.4, 0.3 };
            double[] probs = new double[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95 };
            double target = 40d;

            NormalContinuousDistribution dist = new NormalContinuousDistribution(vals);
            NormalDiscreteDistribution dist2 = new NormalDiscreteDistribution(discreteVals, discreteWeights);
            
            StringBuilder pProfile = new StringBuilder();

            label1.Text = dist.Mean.ToString("0.0000");
            label2.Text = Math.Sqrt(dist.Variance).ToString("0.0000");
            label3.Text = dist.Skewness.ToString("0.0000");
            label4.Text = dist.Kurtosis.ToString("0.0000");
            label5.Text = dist2.Mean.ToString("0.0000");
            label6.Text = Math.Sqrt(dist2.Variance).ToString("0.0000");
            label7.Text = dist2.Skewness.ToString("0.0000");
            label8.Text = dist2.Kurtosis.ToString("0.0000");

            foreach (double prob in probs)
                pProfile.AppendFormat("P{0}     {1}      {2}\n", prob.ToString("00"), dist.InverseCDF(prob / 100).ToString("0.00"), dist2.InverseCDF(prob / 100).ToString("0.00"));
            pProfile.AppendFormat("\nProbability of achieving target: {0}      {1}", ((1d - dist.CDF(target)) * 100).ToString("0.00"), ((1d - dist2.CDF(target)) * 100).ToString("0.00"));

            MessageBox.Show(pProfile.ToString());
        }
    }
}