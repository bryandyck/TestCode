using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CodeProject
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private CodeProject.DDE.DDEListener ddeListener1;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		
			// 
			// ddeListener1
			// 
			this.ddeListener1 = new CodeProject.DDE.DDEListener();
			this.ddeListener1.ActionName = "System";
			this.ddeListener1.AppName = "CPDDETest";
			this.ddeListener1.OnDDEExecute += new CodeProject.DDE.DDEExecuteEventHandler(this.ddeListener1_OnDDEExecute);

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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(416, 128);
			this.label1.TabIndex = 0;
			this.label1.Text = @"You must first accociate a File Type with this application! In Explorer Extra/Folder options goto File Types and add a command to a extension e.p. .txt Enter the path to this executable and check DDE, then enter a command (e.p. [open(""%1"")]) an application name: CPDDETest and an Action Name: System. Then start this Program and open a file in explorer. A MessageBox should now show the command (%1 is replaced with the file name)";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 350);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1});
			this.Name = "Form1";
			this.Text = "Form1";
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

		private void ddeListener1_OnDDEExecute(object Sender, string[] Commands)
		{
			string s="";
			foreach (string s2 in Commands)
			{
				s+=s2;
			}
			MessageBox.Show(this,s);
		}
	}
}
