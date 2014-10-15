namespace WindowsFormsApplication5
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstCurrencies = new System.Windows.Forms.ListBox();
            this.txtEnum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSmall = new System.Windows.Forms.TextBox();
            this.txtMed = new System.Windows.Forms.TextBox();
            this.txtLarge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVisible = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstCurrencies
            // 
            this.lstCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstCurrencies.FormattingEnabled = true;
            this.lstCurrencies.Location = new System.Drawing.Point(16, 21);
            this.lstCurrencies.Name = "lstCurrencies";
            this.lstCurrencies.Size = new System.Drawing.Size(206, 407);
            this.lstCurrencies.TabIndex = 0;
            this.lstCurrencies.SelectedIndexChanged += new System.EventHandler(this.lstCurrencies_SelectedIndexChanged);
            // 
            // txtEnum
            // 
            this.txtEnum.Location = new System.Drawing.Point(364, 43);
            this.txtEnum.Name = "txtEnum";
            this.txtEnum.ReadOnly = true;
            this.txtEnum.Size = new System.Drawing.Size(94, 20);
            this.txtEnum.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enum value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Large string";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Med String";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Small string";
            // 
            // txtSmall
            // 
            this.txtSmall.Location = new System.Drawing.Point(364, 92);
            this.txtSmall.Name = "txtSmall";
            this.txtSmall.ReadOnly = true;
            this.txtSmall.Size = new System.Drawing.Size(94, 20);
            this.txtSmall.TabIndex = 6;
            // 
            // txtMed
            // 
            this.txtMed.Location = new System.Drawing.Point(364, 121);
            this.txtMed.Name = "txtMed";
            this.txtMed.ReadOnly = true;
            this.txtMed.Size = new System.Drawing.Size(94, 20);
            this.txtMed.TabIndex = 7;
            // 
            // txtLarge
            // 
            this.txtLarge.Location = new System.Drawing.Point(364, 151);
            this.txtLarge.Name = "txtLarge";
            this.txtLarge.ReadOnly = true;
            this.txtLarge.Size = new System.Drawing.Size(94, 20);
            this.txtLarge.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Visible by default:";
            // 
            // txtVisible
            // 
            this.txtVisible.Location = new System.Drawing.Point(364, 196);
            this.txtVisible.Name = "txtVisible";
            this.txtVisible.ReadOnly = true;
            this.txtVisible.Size = new System.Drawing.Size(94, 20);
            this.txtVisible.TabIndex = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(29, 440);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Filter list";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 468);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtVisible);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLarge);
            this.Controls.Add(this.txtMed);
            this.Controls.Add(this.txtSmall);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEnum);
            this.Controls.Add(this.lstCurrencies);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstCurrencies;
        private System.Windows.Forms.TextBox txtEnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSmall;
        private System.Windows.Forms.TextBox txtMed;
        private System.Windows.Forms.TextBox txtLarge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVisible;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

