using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ReflectionLearningExample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdReflectClass;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSomeValue;
		private System.Windows.Forms.ListBox lstAttributes;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox lstFields;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox lstProperties;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox lstMethods;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button cmdMyTestMethod3;
		private System.Windows.Forms.Button cmdMyTestMethod4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdReflectClass = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSomeValue = new System.Windows.Forms.TextBox();
			this.lstAttributes = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lstFields = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lstProperties = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lstMethods = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.cmdMyTestMethod3 = new System.Windows.Forms.Button();
			this.cmdMyTestMethod4 = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdReflectClass
			// 
			this.cmdReflectClass.Location = new System.Drawing.Point(22, 14);
			this.cmdReflectClass.Name = "cmdReflectClass";
			this.cmdReflectClass.Size = new System.Drawing.Size(204, 34);
			this.cmdReflectClass.TabIndex = 0;
			this.cmdReflectClass.Text = "Reflect MyTestClass";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(30, 60);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(618, 304);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.lstAttributes);
			this.tabPage1.Controls.Add(this.txtSomeValue);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(610, 278);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Attributes";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.lstFields);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(610, 258);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Fields";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.label7);
			this.tabPage3.Controls.Add(this.lstProperties);
			this.tabPage3.Controls.Add(this.label5);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(610, 258);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Properties";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.label8);
			this.tabPage4.Controls.Add(this.lstMethods);
			this.tabPage4.Controls.Add(this.label9);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(610, 258);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Methods";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Attributes:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(236, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 52);
			this.label2.TabIndex = 1;
			this.label2.Text = "Value of Property called \"Some Value\":";
			// 
			// txtSomeValue
			// 
			this.txtSomeValue.Location = new System.Drawing.Point(340, 18);
			this.txtSomeValue.Name = "txtSomeValue";
			this.txtSomeValue.Size = new System.Drawing.Size(118, 20);
			this.txtSomeValue.TabIndex = 2;
			this.txtSomeValue.Text = "textBox1";
			// 
			// lstAttributes
			// 
			this.lstAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lstAttributes.Location = new System.Drawing.Point(18, 26);
			this.lstAttributes.Name = "lstAttributes";
			this.lstAttributes.Size = new System.Drawing.Size(196, 199);
			this.lstAttributes.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.Location = new System.Drawing.Point(16, 238);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(202, 32);
			this.label3.TabIndex = 4;
			this.label3.Text = "Click on attribute to get value of property labeled \"Some Value\"";
			// 
			// lstFields
			// 
			this.lstFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstFields.Location = new System.Drawing.Point(18, 36);
			this.lstFields.Name = "lstFields";
			this.lstFields.Size = new System.Drawing.Size(568, 173);
			this.lstFields.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(438, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Fields (Data type in brackets):";
			// 
			// lstProperties
			// 
			this.lstProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstProperties.Location = new System.Drawing.Point(18, 38);
			this.lstProperties.Name = "lstProperties";
			this.lstProperties.Size = new System.Drawing.Size(564, 173);
			this.lstProperties.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(448, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Properties (Get/Set/Datatype in brackets):";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Location = new System.Drawing.Point(18, 220);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(442, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "i.e. MyField (int)";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.Location = new System.Drawing.Point(20, 222);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(414, 16);
			this.label7.TabIndex = 7;
			this.label7.Text = "i.e. MyProperty (get/set/int)";
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.Location = new System.Drawing.Point(16, 218);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(205, 16);
			this.label8.TabIndex = 10;
			this.label8.Text = "i.e. void MyMethod(string,int,bool)";
			// 
			// lstMethods
			// 
			this.lstMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstMethods.Location = new System.Drawing.Point(14, 32);
			this.lstMethods.Name = "lstMethods";
			this.lstMethods.Size = new System.Drawing.Size(574, 173);
			this.lstMethods.TabIndex = 9;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(232, 16);
			this.label9.TabIndex = 8;
			this.label9.Text = "Methods (parameter types in brackets):";
			// 
			// cmdMyTestMethod3
			// 
			this.cmdMyTestMethod3.Location = new System.Drawing.Point(238, 12);
			this.cmdMyTestMethod3.Name = "cmdMyTestMethod3";
			this.cmdMyTestMethod3.Size = new System.Drawing.Size(194, 40);
			this.cmdMyTestMethod3.TabIndex = 2;
			this.cmdMyTestMethod3.Text = "Call MyTestMethod3 and raise message box with value returned.";
			this.cmdMyTestMethod3.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdMyTestMethod4
			// 
			this.cmdMyTestMethod4.Location = new System.Drawing.Point(440, 12);
			this.cmdMyTestMethod4.Name = "cmdMyTestMethod4";
			this.cmdMyTestMethod4.Size = new System.Drawing.Size(198, 40);
			this.cmdMyTestMethod4.TabIndex = 3;
			this.cmdMyTestMethod4.Text = "Call MyTestMethod4";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(670, 380);
			this.Controls.Add(this.cmdMyTestMethod4);
			this.Controls.Add(this.cmdMyTestMethod3);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.cmdReflectClass);
			this.Name = "Form1";
			this.Text = "Form1";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

	}
}
