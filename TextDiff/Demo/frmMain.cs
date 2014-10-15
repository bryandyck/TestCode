using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BizKit.TextDiff;
using System.IO;

namespace Demo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox edText2;
		private System.Windows.Forms.TextBox edText1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListView lvCompared;
		private System.Windows.Forms.ColumnHeader columnX;
		private System.Windows.Forms.ColumnHeader columnY;
		private System.Windows.Forms.ColumnHeader columnRange;
		private System.Windows.Forms.ColumnHeader columnKind;
		private System.Windows.Forms.ColumnHeader columnString;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
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
			this.edText2 = new System.Windows.Forms.TextBox();
			this.edText1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.lvCompared = new System.Windows.Forms.ListView();
			this.columnKind = new System.Windows.Forms.ColumnHeader();
			this.columnX = new System.Windows.Forms.ColumnHeader();
			this.columnY = new System.Windows.Forms.ColumnHeader();
			this.columnRange = new System.Windows.Forms.ColumnHeader();
			this.columnString = new System.Windows.Forms.ColumnHeader();
			this.button2 = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.button3 = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// edText2
			// 
			this.edText2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edText2.Location = new System.Drawing.Point(72, 28);
			this.edText2.Name = "edText2";
			this.edText2.Size = new System.Drawing.Size(316, 20);
			this.edText2.TabIndex = 6;
			this.edText2.Text = "Text 2";
			// 
			// edText1
			// 
			this.edText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edText1.Location = new System.Drawing.Point(72, 4);
			this.edText1.Name = "edText1";
			this.edText1.Size = new System.Drawing.Size(316, 20);
			this.edText1.TabIndex = 5;
			this.edText1.Text = "Text 1";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(312, 56);
			this.button1.Name = "button1";
			this.button1.TabIndex = 7;
			this.button1.Text = "Compare";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// lvCompared
			// 
			this.lvCompared.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.columnKind,
																						 this.columnX,
																						 this.columnY,
																						 this.columnRange,
																						 this.columnString});
			this.lvCompared.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lvCompared.FullRowSelect = true;
			this.lvCompared.GridLines = true;
			this.lvCompared.Location = new System.Drawing.Point(0, 85);
			this.lvCompared.Name = "lvCompared";
			this.lvCompared.Size = new System.Drawing.Size(392, 188);
			this.lvCompared.TabIndex = 8;
			this.lvCompared.View = System.Windows.Forms.View.Details;
			this.lvCompared.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// columnKind
			// 
			this.columnKind.Text = "Kind";
			// 
			// columnX
			// 
			this.columnX.Text = "Where";
			this.columnX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// columnY
			// 
			this.columnY.Text = "What";
			this.columnY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// columnRange
			// 
			this.columnRange.Text = "Range";
			this.columnRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// columnString
			// 
			this.columnString.Text = "Convert Src -> Dst";
			this.columnString.Width = 150;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(152, 56);
			this.button2.Name = "button2";
			this.button2.TabIndex = 9;
			this.button2.Text = "Save";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "sav";
			this.saveFileDialog.Filter = "All files|*.*";
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(232, 56);
			this.button3.Name = "button3";
			this.button3.TabIndex = 10;
			this.button3.Text = "Open";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "sav";
			this.openFileDialog.Filter = "All files|*.*";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 16);
			this.label1.TabIndex = 11;
			this.label1.Text = "Source:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 12;
			this.label2.Text = "Destination:";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 273);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.lvCompared);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.edText2);
			this.Controls.Add(this.edText1);
			this.Name = "frmMain";
			this.Text = "TextDiff Demo";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			TextDiffer td = new TextDiffer(); 
			td.Execute(edText1.Text, edText2.Text);
			lvCompared.Items.Clear();
			foreach (ChangeRec cl in td.ChangeList)
			{
				//lbChanges.Items.Add(cl.Kind.ToString() + " " + cl.x.ToString() + " " + cl.y.ToString() + " " + cl.Range.ToString());
				ListViewItem lvi = lvCompared.Items.Add(cl.Kind.ToString());
				lvi.SubItems.Add(cl.x.ToString());
				lvi.SubItems.Add(cl.y.ToString());
				lvi.SubItems.Add(cl.Range.ToString());
				string first = "";
				string second = "";
				switch (cl.Kind)
				{
					case ChangeKind.Modify:
						first = edText1.Text.Substring(cl.x, cl.Range);
						second = edText2.Text.Substring(cl.y, cl.Range);
						lvi.SubItems.Add("\"" + first + "\" -> \"" + second + "\"");
						break;
					case ChangeKind.Delete:
						first = edText1.Text.Substring(cl.x, cl.Range);
						lvi.SubItems.Add("\"" + first + "\"");
						break;
					case ChangeKind.Add:
						first = edText2.Text.Substring(cl.y, cl.Range);
						lvi.SubItems.Add("\"" + first + "\"");
						break;
					default:
						lvi.SubItems.Add("???");
						break;
				}
			}
		}

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				StreamWriter sr = File.CreateText(saveFileDialog.FileName);
				try
				{
					sr.WriteLine (edText1.Text);
					sr.WriteLine (edText2.Text);
				}
				finally
				{
					sr.Close();
				}
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				StreamReader sr = File.OpenText(openFileDialog.FileName);
				try
				{
					edText1.Text = sr.ReadLine ();
					edText2.Text = sr.ReadLine ();
				}
				finally
				{
					sr.Close();
				}
			}
		}
	}
}
