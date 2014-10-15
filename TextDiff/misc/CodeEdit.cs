using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

namespace BizKit.TextDiff
{
	public struct CharFormat2
	{
		public int cbSize;
		public int dwMask;
		public int dwEffects;
		public int yHeight;
		public int yOffset;
		public int crTextColor;
		public byte bCharSet;
		public byte bPitchAndFamily;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
		public string szFaceName;
		public short wWeight;
		public short sSpacing;
		public int crBackColor;
		public int lcid;
		public int dwReserved;
		public short sStyle;
		public short wKerning;
		public byte bUnderlineType;
		public byte bAnimation;
		public byte bRevAuthor;
		public byte bReserved1;
	}

	public class Utils
	{
		#region ScrollEventType to SB_* messages and SB_* messages to ScrollEventType
		
		public static int GetSBFromScrollEventType(ScrollEventType type)
		{
			int res = -1;
			switch (type)
			{
				case ScrollEventType.SmallDecrement:
					res = Utils.SB_LINEUP;
					break;
				case ScrollEventType.SmallIncrement:
					res = Utils.SB_LINEDOWN;
					break;
				case ScrollEventType.LargeDecrement:
					res = Utils.SB_PAGEUP;
					break;
				case ScrollEventType.LargeIncrement:
					res = Utils.SB_PAGEDOWN;
					break;
				case ScrollEventType.ThumbTrack:
					res = Utils.SB_THUMBTRACK;
					break;
				case ScrollEventType.First:
					res = Utils.SB_TOP;
					break;
				case ScrollEventType.Last:
					res = Utils.SB_BOTTOM;
					break;
				case ScrollEventType.ThumbPosition:
					res = Utils.SB_THUMBPOSITION;
					break;
				case ScrollEventType.EndScroll:
					res = Utils.SB_ENDSCROLL;
					break;
				default:
					break;
			}
			return res;
		}

		public static ScrollEventType GetScrollEventType(System.IntPtr wParam)
		{
			ScrollEventType res = 0;
			switch (Utils.LoWord((int)wParam)) 
			{
				case Utils.SB_LINEUP:
					res = ScrollEventType.SmallDecrement;
					break;
				case Utils.SB_LINEDOWN:
					res = ScrollEventType.SmallIncrement;
					break;
				case Utils.SB_PAGEUP:
					res = ScrollEventType.LargeDecrement;
					break;
				case Utils.SB_PAGEDOWN:
					res = ScrollEventType.LargeIncrement;
					break;
				case Utils.SB_THUMBTRACK:
					res = ScrollEventType.ThumbTrack;
					break;
				case Utils.SB_TOP:
					res = ScrollEventType.First;
					break;
				case Utils.SB_BOTTOM:
					res = ScrollEventType.Last;
					break;
				case Utils.SB_THUMBPOSITION:
					res = ScrollEventType.ThumbPosition;
					break;
				case Utils.SB_ENDSCROLL:
					res = ScrollEventType.EndScroll;
					break;
				default:
					res = ScrollEventType.EndScroll;
					break;
			}
			return res;
		}

		#endregion

		#region Const

		public struct SCROLLINFO
		{
			public int cbSize;
			public ScrollBarInfoFlags fMask;
			public int nMin;
			public int nMax;
			public int nPage;
			public int nPos;
			public int nTrackPos;
		}

		public enum ScrollBarFlags	
		{
			SBS_HORZ = 0,
			SBS_VERT = 1,
			SBS_TOPALIGN = 2,
			SBS_LEFTALIGN = 2,
			SBS_BOTTOMALIGN = 4,
			SBS_RIGHTALIGN = 4,
			SBS_SIZEBOXTOPLEFTALIGN = 2,
			SBS_SIZEBOXBOTTOMRIGHTALIGN = 4,
			SBS_SIZEBOX = 8,
			SBS_SIZEGRIP = 16,
		}

		public enum ScrollBarInfoFlags
		{
			SIF_RANGE = 1,
			SIF_PAGE = 2,
			SIF_POS = 4,
			SIF_DISABLENOSCROLL = 8,
			SIF_TRACKPOS = 16,
			SIF_ALL = 23,
		}

		public const int LF_FACESIZE = 32;
		public const int CFM_BACKCOLOR = 67108864;
		public const int CFE_AUTOBACKCOLOR = 67108864;
		public const int WM_USER = 1024;
		public const int EM_SETCHARFORMAT = 1092;
		public const int EM_SETBKGNDCOLOR = 1091;
		public const int EM_GETCHARFORMAT = 1082;
		public const int WM_SETTEXT = 12;

		public const int SB_LINEUP = 0;
		public const int SB_LINEDOWN = 1; 
		public const int SB_PAGEUP = 2; 
		public const int SB_PAGEDOWN = 3;
		public const int SB_THUMBPOSITION = 4; 
		public const int SB_THUMBTRACK = 5; 
		public const int SB_TOP = 6; 
		public const int SB_BOTTOM = 7; 
		public const int SB_ENDSCROLL = 8; 

		public const int WM_HSCROLL = 0x114;
		public const int WM_VSCROLL = 0x115;
		public const int WM_MOUSEWHEEL = 0x020A;
		public const int WM_NCCALCSIZE =0x0083;
		public const int WM_PAINT =0x000F;
		public const int WM_SIZE =0x0005;

		public const int SB_HORZ = 0; 
		public const int SB_VERT = 1; 
		public const int SB_CTL = 2; 
		public const int SB_BOTH = 3; 

		public const uint  ESB_DISABLE_BOTH = 0x3;
		public const uint  ESB_ENABLE_BOTH = 0x0;

		public const int  MK_LBUTTON = 0x01;
		public const int  MK_RBUTTON = 0x02;
		public const int  MK_SHIFT = 0x04;
		public const int  MK_CONTROL = 0x08;
		public const int  MK_MBUTTON = 0x10; 
		public const int  MK_XBUTTON1 = 0x0020;
		public const int  MK_XBUTTON2 = 0x0040;

		#endregion
		#region API32 functions

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int GetSystemMetrics(int code);

		[DllImport("user32.dll")]
		public static extern bool EnableScrollBar(System.IntPtr hWnd, uint wSBflags, uint wArrows);

		[DllImportAttribute("User32", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=true)]
		public static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, ref SCROLLINFO lpsi);

		[DllImportAttribute("User32", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=true)]
		public static extern int SetScrollInfo(IntPtr hWnd, int fnBar, SCROLLINFO ScrollInfo, bool Redraw);

		[DllImport("user32.dll")]
		public static extern int SetScrollRange(System.IntPtr hWnd, int nBar, int nMinPos, int nMaxPos, bool bRedraw);

		[DllImport("user32.dll")]
		public static extern int SetScrollPos(System.IntPtr hWnd, int nBar, int nPos, bool bRedraw);
		
		[DllImport("user32.dll")]
		public static extern int GetScrollPos(System.IntPtr hWnd, int nBar);
		
		[DllImport("user32.dll")]
		public static extern bool ShowScrollBar(System.IntPtr hWnd, int wBar, bool bShow);
		
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

		[DllImportAttribute("user32", EntryPoint="SendMessageA", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, ref CharFormat2 lParam);

		[DllImportAttribute("user32", EntryPoint="SendMessageA", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, Point lParam);

		[DllImportAttribute("user32", EntryPoint="SendMessageA", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		[DllImportAttribute("User32",  EntryPoint="PostMessageA", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=true)]
		public static extern bool PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern int HIWORD(System.IntPtr wParam);

		public static int MakeLong(int LoWord, int HiWord) 
		{ 
			return (HiWord << 16) | (LoWord & 0xffff); 
		} 
 
		public static IntPtr MakeLParam(int LoWord, int HiWord) 
		{ 
			return (IntPtr) ((HiWord << 16) | (LoWord & 0xffff)); 
		}
													  
		public static int HiWord(int number) 
		{
			if ((number & 0x80000000) == 0x80000000)
				return (number >> 16); 
			else
				return (number >> 16) & 0xffff ; 
		}	
		
		public static int LoWord(int number) 
		{ 
			return number & 0xffff; 
		}

		#endregion
	}

	/// <summary>
	/// Summary description for CodeEdit.
	/// </summary>
	public class TCodeEdit : RichTextBox 
	{
		public const int SCF_SELECTION = 1;

		private TLines _Lines = null;
		private Color fLineModClr = Color.ForestGreen;
		public TCodeEdit()
		{
			_Lines = new TLines(this);
			this.BackColor = SystemColors.Window;
		}

		#region Delegates & events
		
		public event System.Windows.Forms.ScrollEventHandler ScrollHorizontal;
		public event System.Windows.Forms.ScrollEventHandler ScrollVertical;
		public event System.Windows.Forms.MouseEventHandler  ScrollMouseWheel;

		#endregion

		#region Vars
		
		private bool _enableHScrollBar = true;
		private bool _enableVScrollBar = true;
		private bool _visibleHScrollBar = true;
		private bool _visibleVScrollBar = true;

		/*private int autoScrollHorizontalMinimum = 0;
		private int autoScrollHorizontalMaximum = 100;

		private int autoScrollVerticalMinimum = 0;
		private int autoScrollVerticalMaximum = 100;*/

		#endregion

		#region Properties

		public int VScrollBarPos
		{
			get
			{
				return Math.Max(VScrollBarInfo.nTrackPos, VScrollBarInfo.nPos);
				//return GetScrollPos(this.Handle, SB_VERT);
			}
			set
			{
				if (VScrollBarPos != value)
				{
					if (Utils.SetScrollPos(base.Handle, Utils.SB_VERT, value, true) != -1)
					{
						Utils.SendMessage(base.Handle, Utils.WM_VSCROLL, Utils.SB_THUMBPOSITION + 0x10000 * value, 0);
						//PostMessage(base.Handle, WM_VSCROLL, SB_THUMBPOSITION + 0x10000 * value, 0);
					}
				}
			}
		}

		public int HScrollBarPos
		{
			get
			{
				return Math.Max(HScrollBarInfo.nTrackPos, HScrollBarInfo.nPos);
				//return GetScrollPos(this.Handle, SB_HORZ);
			}
			set
			{
				if (HScrollBarPos != value)
				{
					if (Utils.SetScrollPos(base.Handle, Utils.SB_HORZ, value, true) != -1)
					{
						Utils.SendMessage(base.Handle, Utils.WM_HSCROLL, Utils.SB_THUMBPOSITION + 0x10000 * value, 0);
					}
				}
			}
		}

		public Utils.SCROLLINFO VScrollBarInfo
		{
			get
			{
				Utils.SCROLLINFO si = new Utils.SCROLLINFO();
				si.fMask = Utils.ScrollBarInfoFlags.SIF_ALL;//POS;
				si.cbSize = Marshal.SizeOf(si);
				Utils.GetScrollInfo(this.Handle, Utils.SB_VERT, ref si);
				return si;
			}
			set
			{
				int res = Utils.SetScrollInfo(this.Handle, Utils.SB_VERT, value, true);
			}
		}

		public Utils.SCROLLINFO HScrollBarInfo
		{
			get
			{
				Utils.SCROLLINFO si = new Utils.SCROLLINFO();
				si.fMask = Utils.ScrollBarInfoFlags.SIF_ALL;//POS;
				si.cbSize = Marshal.SizeOf(si);
				Utils.GetScrollInfo(this.Handle, Utils.SB_HORZ, ref si);
				return si;
			}
			set
			{
				int res = Utils.SetScrollInfo(this.Handle, Utils.SB_HORZ, value, true);
			}
		}

		public Color SelectionBackColor
		{
			get
			{
				// We need to ask the RTB for the backcolor of the current selection.
				// This is done using SendMessage with a format structure which the RTB will fill in for us.
				IntPtr hwnd = base.Handle; // Force the creation of the window handle...
				CharFormat2 charFormat2 = new CharFormat2();
				charFormat2.dwMask = Utils.CFM_BACKCOLOR;
				charFormat2.cbSize = Marshal.SizeOf(charFormat2);
				Utils.SendMessage(base.Handle, Utils.EM_GETCHARFORMAT, SCF_SELECTION, ref charFormat2);
				return ColorTranslator.FromOle(charFormat2.crBackColor);
			}
			set
			{
				// Here we do relatively the same thing as in Get, but we are telling the RTB to set
				// the color this time instead of returning it to us.
				IntPtr hwnd = base.Handle; // Force the creation of the window handle...
				CharFormat2 charFormat2 = new CharFormat2();
				charFormat2.crBackColor = ColorTranslator.ToOle(value);
				charFormat2.dwMask = Utils.CFM_BACKCOLOR;
				charFormat2.cbSize = Marshal.SizeOf(charFormat2);
				Utils.SendMessage(base.Handle, Utils.EM_SETCHARFORMAT, SCF_SELECTION, ref charFormat2);
			}
		}

		public Color ModifiedLineColor
		{
			get
			{
				return fLineModClr;
			}
			set
			{
				fLineModClr = value;
			}
		}

		private bool fAutoLineNums = false;
		public bool AutoLineNums
		{
			get
			{
				return fAutoLineNums;
			}
			set
			{
				fAutoLineNums = value;
			}
		}

		public new TLines Lines
		{
			get
			{
				return _Lines;
			}
			set 
			{
				_Lines = value;
			}
		}

		public bool EnableHScrollBar
		{
			get { return this._enableHScrollBar;  }
			set 
			{ 
				this._enableHScrollBar = value;
				if (value)
					Utils.EnableScrollBar(this.Handle, Utils.SB_HORZ, Utils.ESB_ENABLE_BOTH);
				else
					Utils.EnableScrollBar(this.Handle, Utils.SB_HORZ, Utils.ESB_DISABLE_BOTH);
			}
		}

		public bool EnableVScrollBar
		{
			get { return this._enableVScrollBar;  }
			set 
			{ 
				this._enableVScrollBar = value; 
				if (value)
					Utils.EnableScrollBar(this.Handle, Utils.SB_VERT, Utils.ESB_ENABLE_BOTH);
				else
					Utils.EnableScrollBar(this.Handle, Utils.SB_VERT, Utils.ESB_DISABLE_BOTH);
			}
		}

		public bool VisibleHScrollBar
		{
			get { return this._visibleHScrollBar; }
			set
			{
				this._visibleHScrollBar = value;
				Utils.ShowScrollBar(this.Handle, (int)Utils.SB_HORZ, value);
			}
		}

		public bool VisibleVScrollBar
		{
			get { return this._visibleVScrollBar; }
			set
			{
				this._visibleVScrollBar = value;
				Utils.ShowScrollBar(this.Handle, (int)Utils.SB_VERT, value);
			}
		}

		#endregion

		#region WndProd override

		protected override void WndProc(ref Message msg)
		{
			base.WndProc(ref msg);
			if (msg.HWnd != this.Handle)
				return;
			switch (msg.Msg)
			{
				case Utils.WM_MOUSEWHEEL:
					if (!this.VisibleVScrollBar)
						return;
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
						this.OnVScroll(new EventArgs());
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
		}

		#endregion

		#region Perform Manuall scrolling

		public void ScrollToBottom()
		{
			System.Windows.Forms.Message msg = 
				Message.Create( this.Handle, Utils.WM_VSCROLL, (IntPtr)Utils.SB_BOTTOM, IntPtr.Zero );
			this.DefWndProc( ref msg );
		}

		public void performScrollHorizontal(ScrollEventType type)
		{
			int param = Utils.GetSBFromScrollEventType(type);
			if (param == -1)
				return;
			Utils.SendMessage(this.Handle, (uint)Utils.WM_HSCROLL, (System.UIntPtr)param, (System.IntPtr)0);
		}

		public void performScrollVertical(ScrollEventType type)
		{
			int param = Utils.GetSBFromScrollEventType(type);
			if (param == -1)
				return;
			Utils.SendMessage(this.Handle, (uint)Utils.WM_VSCROLL, (System.UIntPtr)param, (System.IntPtr)0);	
		}

		#endregion


		//[DllImportAttribute("user32", EntryPoint="SendMessageA", CharSet=CharSet.Auto, SetLastError=true)]
		//private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=true)]
		private static extern bool LockWindowUpdate(IntPtr hWndLock);

		public void ClearBackColor(bool ClearAll)
		{
			IntPtr i1 = base.Handle;
			LockWindowUpdate(base.Handle);
			base.SuspendLayout();
			int ScrollPosVert = VScrollBarPos;
			int ScrollPosHoriz = HScrollBarPos;
			int SelStart = base.SelectionStart;
			int SelLength = base.SelectionLength;
			if (ClearAll)
			{
				base.SelectAll();
			}
			CharFormat2 charFormat2 = new CharFormat2();
			charFormat2.crBackColor = -1;
			charFormat2.dwMask = Utils.CFM_BACKCOLOR;
			charFormat2.dwEffects = Utils.CFE_AUTOBACKCOLOR; // Clears the backcolor
			charFormat2.cbSize = Marshal.SizeOf(charFormat2);
			Utils.SendMessage(base.Handle, Utils.EM_SETCHARFORMAT, SCF_SELECTION, ref charFormat2);
            // Return the previous values...
			/*base.SelectionStart = SelStart;
			base.SelectionLength = SelLength;
			Point pnt = new Point();
			pnt.X = ScrollPosHoriz;
			pnt.Y = ScrollPosVert;
			SendMessage(base.Handle, (int)EMFlags.EM_SETSCROLLPOS, 0, pnt);*/
			base.ResumeLayout();
			LockWindowUpdate(IntPtr.Zero);
		}
	}

	public class TLine : System.Object
	{
		public TLine()
		{

		}
		private string fValue;
		public string Value
		{
			get
			{
				return fValue;
			}
			set
			{
				fValue = value;
			}
		}

		private Color fBackClr;
		public Color BackClr
		{
			get
			{
				return fBackClr;
			}
			set
			{
				fBackClr = value;
			}
		}
		private long fTag;
		public long Tag
		{
			get
			{
				return fTag;
			}
			set
			{
				fTag = value;
			}
		}

		/*internal bool fLineModified;
		public bool LineModified
		{
			get
			{	
				return fLineModified;
			}
		}
		internal int fLineOffset;
		public int LineOffset
		{
			get
			{
				return fLineOffset;
			}
		}
		internal int fLineLen;
		public int LineLen
		{
			get
			{
				return fLineLen;
			}
		}*/
	}
	
	public class TLines
	{
		const string NEWLINE = "\n";
		private ArrayList _Lines = null;
		internal TCodeEdit fOwner;
		internal bool fModified;
		//internal bool fUpdating;
		public TLines(TCodeEdit owner)
		{
			fOwner = owner;
			_Lines = new ArrayList();
			Clear();
			fModified = false;
		}

		/// <summary>
		/// Gets the number of elements actually contained in the list.
		/// </summary>
		public int Count 
		{
			get 
			{ 
				return _Lines.Count; 
			}
		}

		/// <summary>
		/// Adds an permission object to the end of the list.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int Add(string value)
		{
			return _Lines.Add(value);
		}
	
		public void RemoveAt(int index)
		{
			_Lines.RemoveAt(index);
		}

		public void Insert(int index, TLine value)
		{
			_Lines.Insert(index, value);
		}

		public TLine this[int index]
		{
			get
			{
				return (TLine)_Lines[index];
			}
			set
			{
				_Lines[index] = (TLine)value;
			}
		}

		/// <summary>
		/// Removes all elements from the list.
		/// </summary>
		public void Clear()
		{
			_Lines.Clear();
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the list.
		/// </summary>
		/// <returns>An IEnumerator for the entire list.</returns>
		public IEnumerator GetEnumerator()
		{
			return _Lines.GetEnumerator();
		}
	}
}
