using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Palantir.Common;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowList(false);
        }

        private void lstCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            Currency val = (Currency)EnumHelper.GetEnum(typeof(Currency), lstCurrencies.SelectedItem.ToString());
            txtEnum.Text = val.ToString();
            txtSmall.Text = CurrencyEnumHelper.GetDisplayString(val, Palantir.Framework.ScaleSize.Small);
            txtMed.Text = CurrencyEnumHelper.GetDisplayString(val, Palantir.Framework.ScaleSize.Medium);
            txtLarge.Text = CurrencyEnumHelper.GetDisplayString(val, Palantir.Framework.ScaleSize.Large);
            txtVisible.Text = CurrencyEnumHelper.GetDefaultVisibility(val).ToString();
        }

        private void ShowList(bool showVisibleOnly)
        {
            List<string> vals = new List<string>();
            vals.AddRange(EnumHelper.GetDescriptions(typeof(Currency)));

            lstCurrencies.Items.Clear();
            if (showVisibleOnly)
            {
                for (int i = vals.Count - 1; i >= 0; i--)
                    if (!CurrencyEnumHelper.GetDefaultVisibility((Currency)EnumHelper.GetEnum(typeof(Currency), vals[i])))
                        vals.RemoveAt(i);
            }
            lstCurrencies.Items.AddRange(vals.ToArray());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ShowList(checkBox1.Checked);
        }

    }
}
