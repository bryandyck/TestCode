using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using GlacialComponents.Controls.Common;

namespace BizKit.TextDiff
{
	/// <summary>
	/// Summary description for SyncCodeList.
	/// </summary>
	public class SyncCodeList : System.Windows.Forms.UserControl
	{
		public CodeList CodeList1;
		private System.Windows.Forms.Splitter splitter1;
		public CodeList CodeList2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SyncCodeList()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
			this.CodeList1 = new CodeList();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.CodeList2 = new CodeList();
			this.SuspendLayout();
			// 
			// CodeList1
			// 
			this.CodeList1.BackColor = System.Drawing.SystemColors.Window;
			glColumn1.Name = "Num";
			glColumn1.Text = "Num";
			glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			glColumn1.Width = 40;
			glColumn2.Name = "Script";
			glColumn2.Text = "Column";
			glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.CodeList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
																						  glColumn1,
																						  glColumn2});
			this.CodeList1.Dock = System.Windows.Forms.DockStyle.Left;
			this.CodeList1.HotTrackingColor = System.Drawing.SystemColors.HotTrack;
			this.CodeList1.ImageList = null;
			this.CodeList1.ItemHeight = 17;
			this.CodeList1.Location = new System.Drawing.Point(0, 0);
			this.CodeList1.Name = "CodeList1";
			this.CodeList1.SelectedTextColor = System.Drawing.SystemColors.HighlightText;
			this.CodeList1.SelectionColor = System.Drawing.SystemColors.Highlight;
			this.CodeList1.Size = new System.Drawing.Size(204, 200);
			this.CodeList1.TabIndex = 0;
			this.CodeList1.UnfocusedSelectionColor = System.Drawing.SystemColors.Highlight;
			this.CodeList1.Click += new System.EventHandler(this.CodeList1_Click);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(204, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 200);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// CodeList2
			// 
			this.CodeList2.BackColor = System.Drawing.SystemColors.Window;
			glColumn3.Name = "Num";
			glColumn3.Text = "Num";
			glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			glColumn3.Width = 40;
			glColumn4.Name = "Script";
			glColumn4.Text = "Column";
			glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.CodeList2.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
																						  glColumn3,
																						  glColumn4});
			this.CodeList2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CodeList2.HotTrackingColor = System.Drawing.SystemColors.HotTrack;
			this.CodeList2.ImageList = null;
			this.CodeList2.ItemHeight = 17;
			this.CodeList2.Location = new System.Drawing.Point(207, 0);
			this.CodeList2.Name = "CodeList2";
			this.CodeList2.SelectedTextColor = System.Drawing.SystemColors.HighlightText;
			this.CodeList2.SelectionColor = System.Drawing.SystemColors.Highlight;
			this.CodeList2.Size = new System.Drawing.Size(237, 200);
			this.CodeList2.TabIndex = 2;
			this.CodeList2.UnfocusedSelectionColor = System.Drawing.SystemColors.Highlight;
			// 
			// SyncCodeList
			// 
			this.Controls.Add(this.CodeList2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.CodeList1);
			this.Name = "SyncCodeList";
			this.Size = new System.Drawing.Size(444, 200);
			this.Resize += new System.EventHandler(this.SyncCodeList_Resize);
			this.Load += new System.EventHandler(this.SyncCodeList_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private bool _VSyncVScrollBars = true;
		public bool VSyncVScrollBars
		{
			get
			{
				return _VSyncVScrollBars;
			}
			set
			{
				_VSyncVScrollBars = value;
			}	
		}

		private bool _HSyncVScrollBars = true;
		public bool HSyncVScrollBars
		{
			get
			{
				return _HSyncVScrollBars;
			}
			set
			{
				_HSyncVScrollBars = value;
			}	
		}

		private void OnResize()
		{
			CodeList1.Width = this.Width / 2;
			CodeList2.Width = this.Width / 2;
			CodeList1.Columns[1].Width = CodeList1.Width - CodeList1.Columns[0].Width;
			CodeList2.Columns[1].Width = CodeList2.Width - CodeList2.Columns[0].Width;
		}

		private void HScroll1(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			MessageBox.Show(e.Type.ToString(), e.NewValue.ToString());
		}

		private void VScroll1(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			if (_VSyncVScrollBars)
			{
				CodeList2.vPanelScrollBar.Value = e.NewValue;
				CodeList2.VScroll(ScrollEventType.Last);
				//CodeList2.vPanelScrollBar.Invalidate();
				//CodeList2.vPanelScrollBar.Refresh();
				//CodeList2.vPanelScrollBar.Update();
				//ScrollEventArgs se = new ScrollEventArgs(ScrollEventType.
				//ManagedVScrollBar nsb = CodeList2.vPanelScrollBar.;
				//((ManagedVScrollBarEx)nsb).OnScroll(e);
				//MessageBox.Show(e.Type.ToString(), e.NewValue.ToString());
			}
		}

		private void HScroll2(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			MessageBox.Show(e.Type.ToString(), e.NewValue.ToString());
		}

		private void VScroll2(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			if (_VSyncVScrollBars)
			{
//				CodeList2.vPanelScrollBar.Value = e.NewValue;
				MessageBox.Show(e.Type.ToString(), e.NewValue.ToString());
				//MessageBox.Show(e.Type.ToString(), e.NewValue.ToString());
			}
		}

		private void SyncCodeList_Load(object sender, System.EventArgs e)
		{
			OnResize();
			CodeList1.hPanelScrollBar.Scroll += new ScrollEventHandler(HScroll1);
			CodeList1.vPanelScrollBar.Scroll += new ScrollEventHandler(VScroll1);
			CodeList2.hPanelScrollBar.Scroll += new ScrollEventHandler(HScroll2);
			CodeList2.vPanelScrollBar.Scroll += new ScrollEventHandler(VScroll2);
			//CodeList2.vPanelScrollBar.ValueChanged += new EventHandler(vPanelScrollBar_ValueChanged);
		}

		private void vPanelScrollBar_ValueChanged(object sender, EventArgs e)
		{
			MessageBox.Show("Value changed");
		}

		private void CodeList1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void SyncCodeList_Resize(object sender, System.EventArgs e)
		{
			OnResize();
		}
	}
}
