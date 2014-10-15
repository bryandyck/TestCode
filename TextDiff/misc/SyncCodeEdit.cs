using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace BizKit.TextDiff
{
	/// <summary>
	/// Summary description for SyncCodeEdit.
	/// </summary>
	public class SyncCodeEdit : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SyncCodeEdit()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			CodeEdit1.Text = "";
			CodeEdit2.Text = "";
			OnResize();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gbLeft = new System.Windows.Forms.GroupBox();
			this.CodeEdit1 = new BizKit.TextDiff.TCodeEdit();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.gbRight = new System.Windows.Forms.GroupBox();
			this.CodeEdit2 = new BizKit.TextDiff.TCodeEdit();
			this.gbLeft.SuspendLayout();
			this.gbRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbLeft
			// 
			this.gbLeft.Controls.Add(this.CodeEdit1);
			this.gbLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.gbLeft.Location = new System.Drawing.Point(0, 0);
			this.gbLeft.Name = "gbLeft";
			this.gbLeft.Size = new System.Drawing.Size(212, 228);
			this.gbLeft.TabIndex = 4;
			this.gbLeft.TabStop = false;
			this.gbLeft.Text = "Object 1";
			// 
			// CodeEdit1
			// 
			this.CodeEdit1.AutoLineNums = false;
			this.CodeEdit1.BackColor = System.Drawing.SystemColors.Window;
			this.CodeEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CodeEdit1.EnableHScrollBar = true;
			this.CodeEdit1.EnableVScrollBar = true;
			this.CodeEdit1.HScrollBarPos = 0;
			this.CodeEdit1.Location = new System.Drawing.Point(3, 16);
			this.CodeEdit1.ModifiedLineColor = System.Drawing.Color.ForestGreen;
			this.CodeEdit1.Name = "CodeEdit1";
			this.CodeEdit1.SelectionBackColor = System.Drawing.Color.Black;
			this.CodeEdit1.Size = new System.Drawing.Size(206, 209);
			this.CodeEdit1.TabIndex = 2;
			this.CodeEdit1.Text = "";
			this.CodeEdit1.VisibleHScrollBar = false;
			this.CodeEdit1.VisibleVScrollBar = false;
			this.CodeEdit1.VScrollBarPos = 0;
			this.CodeEdit1.WordWrap = false;
			this.CodeEdit1.VScroll += new System.EventHandler(this.CodeEdit1_VScroll);
			this.CodeEdit1.HScroll += new System.EventHandler(this.CodeEdit1_HScroll);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(212, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 228);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			// 
			// gbRight
			// 
			this.gbRight.Controls.Add(this.CodeEdit2);
			this.gbRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbRight.Location = new System.Drawing.Point(215, 0);
			this.gbRight.Name = "gbRight";
			this.gbRight.Size = new System.Drawing.Size(201, 228);
			this.gbRight.TabIndex = 6;
			this.gbRight.TabStop = false;
			this.gbRight.Text = "Object 2";
			// 
			// CodeEdit2
			// 
			this.CodeEdit2.AutoLineNums = false;
			this.CodeEdit2.BackColor = System.Drawing.SystemColors.Window;
			this.CodeEdit2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CodeEdit2.EnableHScrollBar = true;
			this.CodeEdit2.EnableVScrollBar = true;
			this.CodeEdit2.HScrollBarPos = 0;
			this.CodeEdit2.Location = new System.Drawing.Point(3, 16);
			this.CodeEdit2.ModifiedLineColor = System.Drawing.Color.ForestGreen;
			this.CodeEdit2.Name = "CodeEdit2";
			this.CodeEdit2.SelectionBackColor = System.Drawing.Color.Black;
			this.CodeEdit2.Size = new System.Drawing.Size(195, 209);
			this.CodeEdit2.TabIndex = 4;
			this.CodeEdit2.Text = "";
			this.CodeEdit2.VisibleHScrollBar = false;
			this.CodeEdit2.VisibleVScrollBar = false;
			this.CodeEdit2.VScrollBarPos = 0;
			this.CodeEdit2.WordWrap = false;
			this.CodeEdit2.VScroll += new System.EventHandler(this.CodeEdit2_VScroll);
			this.CodeEdit2.HScroll += new System.EventHandler(this.CodeEdit2_HScroll);
			// 
			// SyncCodeEdit
			// 
			this.Controls.Add(this.gbRight);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.gbLeft);
			this.Name = "SyncCodeEdit";
			this.Size = new System.Drawing.Size(416, 228);
			this.Resize += new System.EventHandler(this.SyncCodeEdit_Resize);
			this.Load += new System.EventHandler(this.SyncCodeEdit_Load);
			this.gbLeft.ResumeLayout(false);
			this.gbRight.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void SyncCodeEdit_Load(object sender, System.EventArgs e)
		{
		}

		private void OnResize()
		{
			gbLeft.Width = this.Width / 2;
			gbRight.Width = this.Width / 2;
		}

		private void SyncCodeEdit_Resize(object sender, System.EventArgs e)
		{
			OnResize();
		}

		private bool _VScrollSync = true;
		private bool _HScrollSync = true;
		private bool _vunlock1 = true;
		private System.Windows.Forms.GroupBox gbLeft;
		public BizKit.TextDiff.TCodeEdit CodeEdit1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.GroupBox gbRight;
		public BizKit.TextDiff.TCodeEdit CodeEdit2;
		private bool _vunlock2 = true;
		private void CodeEdit1_VScroll(object sender, System.EventArgs e)
		{
			if (_VScrollSync && _vunlock1)
			{
				_vunlock2 = false;
				CodeEdit2.VScrollBarPos = CodeEdit1.VScrollBarPos;
				_vunlock2 = true;
			}
		}

		public string DatabaseName1
		{
			get
			{
				return gbLeft.Text;
			}
			set
			{
				gbLeft.Text = value;
			}
		}

		public string DatabaseName2
		{
			get
			{
				return gbRight.Text;
			}
			set
			{
				gbRight.Text = value;
			}
		}

		public string Script1
		{
			get
			{
				return CodeEdit1.Text;
			}
			set
			{
				CodeEdit1.Text = value;
			}
		}

		public string Script2
		{
			get
			{
				return CodeEdit2.Text;
			}
			set
			{
				CodeEdit2.Text = value;
			}
		}

		private void CodeEdit2_VScroll(object sender, System.EventArgs e)
		{
			if (_VScrollSync && _vunlock2)
			{
				_vunlock1 = false;
				CodeEdit1.VScrollBarPos = CodeEdit2.VScrollBarPos;
				_vunlock1 = true;
			}
		}

		private bool _hunlock1 = true;
		private bool _hunlock2 = true;
		private void CodeEdit2_HScroll(object sender, System.EventArgs e)
		{
			if (_HScrollSync && _hunlock2)
			{
				_hunlock1 = false;
				CodeEdit1.HScrollBarPos = CodeEdit2.HScrollBarPos;
				_hunlock1 = true;
			}
		}

		private void CodeEdit1_HScroll(object sender, System.EventArgs e)
		{
			if (_HScrollSync && _hunlock1)
			{
				_hunlock2 = false;
				CodeEdit2.HScrollBarPos = CodeEdit1.HScrollBarPos;
				_hunlock2 = true;
			}
		}
	
		public bool VScrollSync
		{
			get
			{
				return _VScrollSync;
			}
			set
			{
				_VScrollSync = value;
			}
		}

		public bool HScrollSync
		{
			get
			{
				return _HScrollSync;
			}
			set
			{
				_HScrollSync = value;
			}
		}
	}
}
