using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Text;
using GlacialComponents.Controls;
using GlacialComponents.Controls.Common;

namespace BizKit.TextDiff
{
	public class ManagedVScrollBarEx : ManagedVScrollBar
	{
		protected new void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
		}
	}

	/// <summary>
	/// Summary description for CodeList.
	/// </summary>
	public class CodeList : GlacialComponents.Controls.GlacialList
	{
		/*public CodeList()
		{
			//
			// TODO: Add constructor logic here
			//
		}*/
		//public event System.Windows.Forms.ScrollEventHandler ScrollHorizontal;
		//public event System.Windows.Forms.ScrollEventHandler ScrollVertical;
		//public event System.Windows.Forms.MouseEventHandler  ScrollMouseWheel;

		private void gList1_ScrollVertical(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			//MessageBox.Show(e.Type.ToString(), e.NewValue.ToString());
		}

		public CodeList()
		{
			//this.hPanelScrollBar.Scroll += new ScrollEventHandler(gList1_ScrollVertical);
			//this.vPanelScrollBar.Scroll += new ScrollEventHandler(gList1_ScrollVertical);
		}

		private void AddItem(int index, string value)
		{
			GLItem gli = new GLItem();
			gli.SubItems[0].Text = index.ToString();
			gli.SubItems[1].Text = value;
			this.Items.Add(gli);
		}

		protected string[] ToStringArray()
		{
			string[] str = new string[this.Items.Count];
			for (int i = 0; i < this.Items.Count; i++)
			{
				GLItem gli = new GLItem();
				str[i] = gli.SubItems[1].Text;	
			}
			return str;
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			ParseTextToLines();
		}

		private string[] ParseTextToLines()
		{
			string str1 = base.Text;
			//ArrayList arrayList = new ArrayList();
			int j;
			
			for (int i = 0; i < str1.Length; i = j)
			{
				char ch;
				for (j = i; j < str1.Length; j++)
				{
					ch = str1[j];
					if (ch == '\r' || ch == '\n')
					{
						break;
					}
				}
				string str2 = str1.Substring(i, j - i);
				AddItem(i+1, str2);
				//arrayList.Add(str2);
				if (j < str1.Length && str1[j] == '\r')
				{
					j++;
				}
				if (j < str1.Length && str1[j] == '\n')
				{
					j++;
				}
			}
			if (str1.Length > 0 && (str1[str1.Length - 1] == '\r' || str1[str1.Length - 1] == '\n'))
			{
				//arrayList.Add("");
				AddItem(this.Items.Count+1, "");
			}
			this.Invalidate();
			return this.ToStringArray();//(string[])arrayList.ToArray(typeof(string));
		}

		public string[] Lines
		{
			get
			{
				return ParseTextToLines();
			}

			set
			{
				if (value != null && (int)value.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder(value[0]);
					for (int i = 1; i < (int)value.Length; i++)
					{
						stringBuilder.Append("\r\n");
						stringBuilder.Append(value[i]);
					}
					base.Text = stringBuilder.ToString();
					return;
				}
				base.Text = "";
			}
		}

		public void VScroll(ScrollEventType type)
		{
			/*int param = Utils.GetSBFromScrollEventType(type);
			if (param == -1)
				return;
			Utils.SendMessage(this.vPanelScrollBar.Handle, (uint)Utils.WM_VSCROLL, (System.UIntPtr)param, (System.IntPtr)0);	*/
		}

		#region WndProd override

		/*protected override void WndProc(ref Message msg)
		{
			base.WndProc(ref msg);
			if (msg.HWnd != this.Handle)
				return;
			switch (msg.Msg)
			{
				case Utils.WM_MOUSEWHEEL:
					//if (!this.VisibleVScrollBar)
					//	return;
					try
					{
						int zDelta = Utils.HiWord((int)msg.WParam);
						int y = Utils.HiWord((int)msg.LParam);
						int x = Utils.LoWord((int)msg.LParam);
						System.Windows.Forms.MouseButtons butt;
						switch (Utils.LoWord((int)msg.WParam))
						{
							case Utils.MK_LBUTTON:
								butt = System.Windows.Forms.MouseButtons.Left;
								break;
							case Utils.MK_MBUTTON:
								butt = System.Windows.Forms.MouseButtons.Middle;
								break;
							case Utils.MK_RBUTTON:
								butt = System.Windows.Forms.MouseButtons.Right;
								break;
							case Utils.MK_XBUTTON1:
								butt = System.Windows.Forms.MouseButtons.XButton1;
								break;
							case Utils.MK_XBUTTON2:
								butt = System.Windows.Forms.MouseButtons.XButton2;
								break;
							default:
								butt = System.Windows.Forms.MouseButtons.None;
								break;
						}
						System.Windows.Forms.MouseEventArgs arg0 = new System.Windows.Forms.MouseEventArgs(butt, 1, x, y, zDelta);
						this.ScrollMouseWheel(this, arg0);
					}
					catch (Exception) { }
					
					break;

				case Utils.WM_VSCROLL:
					
					try
					{
						ScrollEventType type = Utils.GetScrollEventType(msg.WParam);
						ScrollEventArgs arg = new ScrollEventArgs(type, Utils.GetScrollPos(this.Handle, (int)Utils.SB_VERT));
						//this.OnVScroll(new EventArgs());
						this.ScrollVertical(this, arg);
					}
					catch (Exception) { }
					
					break;

				case Utils.WM_HSCROLL:

					try
					{
						ScrollEventType type = Utils.GetScrollEventType(msg.WParam);
						ScrollEventArgs arg = new ScrollEventArgs(type, Utils.GetScrollPos(this.Handle, (int)Utils.SB_HORZ));
						//this.OnHScroll(new EventArgs());
						this.ScrollHorizontal(this, arg);
					}
					catch (Exception) { }
					
					break;

				default:
					break;
			}
		}*/

		#endregion
	}
}
