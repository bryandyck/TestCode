using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using CodeProject.Win32;
using System.Runtime.InteropServices;

namespace CodeProject.DDE
{
	/// <summary>
	/// Listen for DDE Execute Messages.
	/// </summary>
	public class DDEListener : System.ComponentModel.Component,IDisposable
	{
		private System.ComponentModel.Container components = null;
		//this class inherits Windows.Forms.NativeWindow and provides an Event for message processing
		private DummyWindowWithMessages m_Window=new DummyWindowWithMessages();
		private string m_AppName;
		private string m_ActionName;

		public DDEListener(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			m_Window.ProcessMessage+=new MessageEventHandler(MessageEvent);
		}

		public DDEListener()
		{
			InitializeComponent();
			if (!DesignMode)
			{
				m_Window.ProcessMessage+=new MessageEventHandler(MessageEvent);
			}
		}

		public new void Dispose()
		{
			m_Window.ProcessMessage-=new MessageEventHandler(MessageEvent);
		}

		/// <summary>
		/// Event is fired after WM_DDEExecute
		/// </summary>
		public event DDEExecuteEventHandler OnDDEExecute;

		/// <summary>
		/// The Application Name to listen for
		/// </summary>
		public string AppName
		{
			get{return m_AppName;}
			set{m_AppName=value;}
		}

		/// <summary>
		/// The Action Name to listen for
		/// </summary>
		public string ActionName
		{
			get{return m_ActionName;}
			set{m_ActionName=value;}
		}

	#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		private bool isInitiated=false;

		/// <summary>
		/// Processing the Messages
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="m"></param>
		/// <param name="Handled"></param>
		private void MessageEvent(object sender,ref Message m,ref bool Handled)
		{
			//A client wants to Initiate a DDE connection
			if ((m.Msg==(int)Win32.Msgs.WM_DDE_INITIATE))
			{
				System.Diagnostics.Debug.WriteLine("WM_DDE_INITIATE!");
				//Get the ATOMs for AppName and ActionName
				ushort a1=Win32.Kernel32.GlobalAddAtom(Marshal.StringToHGlobalAnsi(m_AppName));
				ushort a2=Win32.Kernel32.GlobalAddAtom(Marshal.StringToHGlobalAnsi(m_ActionName));
				
				//The LParam of the Message contains the ATOMs for AppName and ActionName
				ushort s1 = (ushort)(((uint)m.LParam) & 0xFFFF);
				ushort s2 = (ushort)((((uint)m.LParam) & 0xFFFF0000) >> 16);
				
				//Return when the ATOMs are not equal.
				if ((a1!=s1)||(a2!=s2)) return;

				//At this point we know that this application should answer, so we send
				//a WM_DDE_ACK message confirming the connection
				IntPtr po=Win32.User32.PackDDElParam((int)Msgs.WM_DDE_ACK,(IntPtr)a1,(IntPtr)a2);
				Win32.User32.SendMessage(m.WParam,(int)Msgs.WM_DDE_ACK,m_Window.Handle,po);
				//Release ATOMs
				Win32.Kernel32.GlobalDeleteAtom(a1);
				Win32.Kernel32.GlobalDeleteAtom(a2);
				isInitiated=true;
				Handled=true;
			}

			//After the connection is established the Client should send a WM_DDE_EXECUTE message
			if ((m.Msg==(int)Win32.Msgs.WM_DDE_EXECUTE))
			{
				System.Diagnostics.Debug.WriteLine("WM_DDE_EXECUTE!");
				//prevent errors
				if (!isInitiated) return;
				//LParam contains the Execute string, so we must Lock the memory block passed and
				//read the string. The Marshal class provides helper functions
				IntPtr pV=Win32.Kernel32.GlobalLock(m.LParam);
				string s3=System.Runtime.InteropServices.Marshal.PtrToStringAuto(pV);
				Win32.Kernel32.GlobalUnlock(m.LParam);
				//After the message has been processed, a WM_DDE_ACK message is sent
				IntPtr lP=Win32.User32.PackDDElParam((int)Win32.Msgs.WM_DDE_ACK,(IntPtr)1,m.LParam);
				Win32.User32.PostMessage(m.WParam,(int)Win32.Msgs.WM_DDE_ACK,m_Window.Handle,lP);
				System.Diagnostics.Debug.WriteLine(s3);
				//now we split the string in Parts (every command should have [] around the text)
				//the client could send multiple commands
				string[] sarr=s3.Split(new char[]{'[',']'});
				if (sarr.GetUpperBound(0)>-1)
				{
					//and fire the event, passing the array of strings
					if (OnDDEExecute!=null) OnDDEExecute(this,sarr);
				}
				Handled=true;
			}

			//After the WM_DDE_EXECUTE message the client should Terminate the connection
			if (m.Msg==(int)Win32.Msgs.WM_DDE_TERMINATE)
			{
				System.Diagnostics.Debug.WriteLine("WM_DDE_TERMINATE");
				if (!isInitiated) return;
				//Confirm termination
				Win32.User32.PostMessage(m.WParam,(int)Win32.Msgs.WM_DDE_TERMINATE,m_Window.Handle,(IntPtr)0);
				Handled=true;
				isInitiated=false;
			}
		}
	
	
		#region supporting classes

		/// <summary>
		/// Summary description for Win32.
		/// </summary>
		private class User32
		{
			public const int GCL_HCURSOR = -12;
			public const int CF_ENHMETAFILE = 14;
			[DllImport("user32.dll")]
			public static extern bool RegisterHotKey(IntPtr hWnd,int id,int fsModifiers,int vlc);
			[DllImport("user32.dll")]
			public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
			[DllImport("user32.dll")]
			public static extern bool UnpackDDElParam(int msg, IntPtr lParam, out IntPtr pLo, out IntPtr pHi);
			[DllImport("user32.Dll")]
			public static extern IntPtr PackDDElParam(int msg, IntPtr pLo, IntPtr pHi);
			[DllImport("user32.Dll")]
			public static extern bool FreeDDElParam(int msg, IntPtr lParam);
			[DllImport("user32.Dll")]
			public static extern IntPtr ReuseDDElParam(IntPtr lParam, int msgIn, int msgOut, IntPtr pLo, IntPtr pHi);
			[DllImport("user32.Dll")]
			public static extern IntPtr SendMessage(IntPtr hWnd,int msg, IntPtr wParam, IntPtr lParam);
			[DllImport("user32.Dll")]
			public static extern bool PostMessage(IntPtr hWnd,int msg, IntPtr wParam, IntPtr lParam);
			[DllImport("User32.dll", CharSet=CharSet.Auto)]
			public static extern uint SetClassLong(IntPtr hWnd, int index, int dwNewLong);
			[DllImport("User32.dll", CharSet=CharSet.Auto)]
			public static extern uint MessageBeep(int uType);
			[DllImport("user32.dll", CharSet=CharSet.Auto)]
			public static extern bool OpenClipboard(IntPtr h);
			[DllImport("user32.dll", CharSet=CharSet.Auto)]
			public static extern bool EmptyClipboard();
			[DllImport("user32.dll", CharSet=CharSet.Auto)]
			public static extern bool SetClipboardData(int type, IntPtr h);
			[DllImport("user32.dll", CharSet=CharSet.Auto)]
			public static extern bool CloseClipboard();
		}

		private class Kernel32
		{
			[DllImport("kernel32.dll")]
			public static extern ushort GlobalAddAtom(IntPtr Name);
			[DllImport("kernel32.dll")]
			public static extern ushort GlobalDeleteAtom(ushort atom);
			[DllImport("kernel32.dll")]
			public static extern IntPtr GlobalLock(IntPtr hMem);
			[DllImport("kernel32.dll")]
			public static extern bool GlobalUnlock(IntPtr hMem);
		}

		private enum Modifiers {MOD_ALT=0x0001,MOD_CONTROL=0x0002,MOD_SHIFT=0x0004,MOD_WIN=0x0008}
		private enum Msgs
		{
			WM_NULL                   = 0x0000,
			WM_CREATE                 = 0x0001,
			WM_DESTROY                = 0x0002,
			WM_MOVE                   = 0x0003,
			WM_SIZE                   = 0x0005,
			WM_ACTIVATE               = 0x0006,
			WM_SETFOCUS               = 0x0007,
			WM_KILLFOCUS              = 0x0008,
			WM_ENABLE                 = 0x000A,
			WM_SETREDRAW              = 0x000B,
			WM_SETTEXT                = 0x000C,
			WM_GETTEXT                = 0x000D,
			WM_GETTEXTLENGTH          = 0x000E,
			WM_PAINT                  = 0x000F,
			WM_CLOSE                  = 0x0010,
			WM_QUERYENDSESSION        = 0x0011,
			WM_QUIT                   = 0x0012,
			WM_QUERYOPEN              = 0x0013,
			WM_ERASEBKGND             = 0x0014,
			WM_SYSCOLORCHANGE         = 0x0015,
			WM_ENDSESSION             = 0x0016,
			WM_SHOWWINDOW             = 0x0018,
			WM_WININICHANGE           = 0x001A,
			WM_SETTINGCHANGE          = 0x001A,
			WM_DEVMODECHANGE          = 0x001B,
			WM_ACTIVATEAPP            = 0x001C,
			WM_FONTCHANGE             = 0x001D,
			WM_TIMECHANGE             = 0x001E,
			WM_CANCELMODE             = 0x001F,
			WM_SETCURSOR              = 0x0020,
			WM_MOUSEACTIVATE          = 0x0021,
			WM_CHILDACTIVATE          = 0x0022,
			WM_QUEUESYNC              = 0x0023,
			WM_GETMINMAXINFO          = 0x0024,
			WM_PAINTICON              = 0x0026,
			WM_ICONERASEBKGND         = 0x0027,
			WM_NEXTDLGCTL             = 0x0028,
			WM_SPOOLERSTATUS          = 0x002A,
			WM_DRAWITEM               = 0x002B,
			WM_MEASUREITEM            = 0x002C,
			WM_DELETEITEM             = 0x002D,
			WM_VKEYTOITEM             = 0x002E,
			WM_CHARTOITEM             = 0x002F,
			WM_SETFONT                = 0x0030,
			WM_GETFONT                = 0x0031,
			WM_SETHOTKEY              = 0x0032,
			WM_GETHOTKEY              = 0x0033,
			WM_QUERYDRAGICON          = 0x0037,
			WM_COMPAREITEM            = 0x0039,
			WM_GETOBJECT              = 0x003D,
			WM_COMPACTING             = 0x0041,
			WM_COMMNOTIFY             = 0x0044 ,
			WM_WINDOWPOSCHANGING      = 0x0046,
			WM_WINDOWPOSCHANGED       = 0x0047,
			WM_POWER                  = 0x0048,
			WM_COPYDATA               = 0x004A,
			WM_CANCELJOURNAL          = 0x004B,
			WM_NOTIFY                 = 0x004E,
			WM_INPUTLANGCHANGEREQUEST = 0x0050,
			WM_INPUTLANGCHANGE        = 0x0051,
			WM_TCARD                  = 0x0052,
			WM_HELP                   = 0x0053,
			WM_USERCHANGED            = 0x0054,
			WM_NOTIFYFORMAT           = 0x0055,
			WM_CONTEXTMENU            = 0x007B,
			WM_STYLECHANGING          = 0x007C,
			WM_STYLECHANGED           = 0x007D,
			WM_DISPLAYCHANGE          = 0x007E,
			WM_GETICON                = 0x007F,
			WM_SETICON                = 0x0080,
			WM_NCCREATE               = 0x0081,
			WM_NCDESTROY              = 0x0082,
			WM_NCCALCSIZE             = 0x0083,
			WM_NCHITTEST              = 0x0084,
			WM_NCPAINT                = 0x0085,
			WM_NCACTIVATE             = 0x0086,
			WM_GETDLGCODE             = 0x0087,
			WM_SYNCPAINT              = 0x0088,
			WM_NCMOUSEMOVE            = 0x00A0,
			WM_NCLBUTTONDOWN          = 0x00A1,
			WM_NCLBUTTONUP            = 0x00A2,
			WM_NCLBUTTONDBLCLK        = 0x00A3,
			WM_NCRBUTTONDOWN          = 0x00A4,
			WM_NCRBUTTONUP            = 0x00A5,
			WM_NCRBUTTONDBLCLK        = 0x00A6,
			WM_NCMBUTTONDOWN          = 0x00A7,
			WM_NCMBUTTONUP            = 0x00A8,
			WM_NCMBUTTONDBLCLK        = 0x00A9,
			WM_KEYDOWN                = 0x0100,
			WM_KEYUP                  = 0x0101,
			WM_CHAR                   = 0x0102,
			WM_DEADCHAR               = 0x0103,
			WM_SYSKEYDOWN             = 0x0104,
			WM_SYSKEYUP               = 0x0105,
			WM_SYSCHAR                = 0x0106,
			WM_SYSDEADCHAR            = 0x0107,
			WM_KEYLAST                = 0x0108,
			WM_IME_STARTCOMPOSITION   = 0x010D,
			WM_IME_ENDCOMPOSITION     = 0x010E,
			WM_IME_COMPOSITION        = 0x010F,
			WM_IME_KEYLAST            = 0x010F,
			WM_INITDIALOG             = 0x0110,
			WM_COMMAND                = 0x0111,
			WM_SYSCOMMAND             = 0x0112,
			WM_TIMER                  = 0x0113,
			WM_HSCROLL                = 0x0114,
			WM_VSCROLL                = 0x0115,
			WM_INITMENU               = 0x0116,
			WM_INITMENUPOPUP          = 0x0117,
			WM_MENUSELECT             = 0x011F,
			WM_MENUCHAR               = 0x0120,
			WM_ENTERIDLE              = 0x0121,
			WM_MENURBUTTONUP          = 0x0122,
			WM_MENUDRAG               = 0x0123,
			WM_MENUGETOBJECT          = 0x0124,
			WM_UNINITMENUPOPUP        = 0x0125,
			WM_MENUCOMMAND            = 0x0126,
			WM_CTLCOLORMSGBOX         = 0x0132,
			WM_CTLCOLOREDIT           = 0x0133,
			WM_CTLCOLORLISTBOX        = 0x0134,
			WM_CTLCOLORBTN            = 0x0135,
			WM_CTLCOLORDLG            = 0x0136,
			WM_CTLCOLORSCROLLBAR      = 0x0137,
			WM_CTLCOLORSTATIC         = 0x0138,
			WM_MOUSEMOVE              = 0x0200,
			WM_LBUTTONDOWN            = 0x0201,
			WM_LBUTTONUP              = 0x0202,
			WM_LBUTTONDBLCLK          = 0x0203,
			WM_RBUTTONDOWN            = 0x0204,
			WM_RBUTTONUP              = 0x0205,
			WM_RBUTTONDBLCLK          = 0x0206,
			WM_MBUTTONDOWN            = 0x0207,
			WM_MBUTTONUP              = 0x0208,
			WM_MBUTTONDBLCLK          = 0x0209,
			WM_MOUSEWHEEL             = 0x020A,
			WM_PARENTNOTIFY           = 0x0210,
			WM_ENTERMENULOOP          = 0x0211,
			WM_EXITMENULOOP           = 0x0212,
			WM_NEXTMENU               = 0x0213,
			WM_SIZING                 = 0x0214,
			WM_CAPTURECHANGED         = 0x0215,
			WM_MOVING                 = 0x0216,
			WM_DEVICECHANGE           = 0x0219,
			WM_MDICREATE              = 0x0220,
			WM_MDIDESTROY             = 0x0221,
			WM_MDIACTIVATE            = 0x0222,
			WM_MDIRESTORE             = 0x0223,
			WM_MDINEXT                = 0x0224,
			WM_MDIMAXIMIZE            = 0x0225,
			WM_MDITILE                = 0x0226,
			WM_MDICASCADE             = 0x0227,
			WM_MDIICONARRANGE         = 0x0228,
			WM_MDIGETACTIVE           = 0x0229,
			WM_MDISETMENU             = 0x0230,
			WM_ENTERSIZEMOVE          = 0x0231,
			WM_EXITSIZEMOVE           = 0x0232,
			WM_DROPFILES              = 0x0233,
			WM_MDIREFRESHMENU         = 0x0234,
			WM_IME_SETCONTEXT         = 0x0281,
			WM_IME_NOTIFY             = 0x0282,
			WM_IME_CONTROL            = 0x0283,
			WM_IME_COMPOSITIONFULL    = 0x0284,
			WM_IME_SELECT             = 0x0285,
			WM_IME_CHAR               = 0x0286,
			WM_IME_REQUEST            = 0x0288,
			WM_IME_KEYDOWN            = 0x0290,
			WM_IME_KEYUP              = 0x0291,
			WM_MOUSEHOVER             = 0x02A1,
			WM_MOUSELEAVE             = 0x02A3,
			WM_CUT                    = 0x0300,
			WM_COPY                   = 0x0301,
			WM_PASTE                  = 0x0302,
			WM_CLEAR                  = 0x0303,
			WM_UNDO                   = 0x0304,
			WM_RENDERFORMAT           = 0x0305,
			WM_RENDERALLFORMATS       = 0x0306,
			WM_DESTROYCLIPBOARD       = 0x0307,
			WM_DRAWCLIPBOARD          = 0x0308,
			WM_PAINTCLIPBOARD         = 0x0309,
			WM_VSCROLLCLIPBOARD       = 0x030A,
			WM_SIZECLIPBOARD          = 0x030B,
			WM_ASKCBFORMATNAME        = 0x030C,
			WM_CHANGECBCHAIN          = 0x030D,
			WM_HSCROLLCLIPBOARD       = 0x030E,
			WM_QUERYNEWPALETTE        = 0x030F,
			WM_PALETTEISCHANGING      = 0x0310,
			WM_PALETTECHANGED         = 0x0311,
			WM_HOTKEY                 = 0x0312,
			WM_PRINT                  = 0x0317,
			WM_PRINTCLIENT            = 0x0318,
			WM_HANDHELDFIRST          = 0x0358,
			WM_HANDHELDLAST           = 0x035F,
			WM_AFXFIRST               = 0x0360,
			WM_AFXLAST                = 0x037F,
			WM_PENWINFIRST            = 0x0380,
			WM_PENWINLAST             = 0x038F,
			WM_APP                    = 0x8000,
			WM_USER                   = 0x0400,
			WM_DDE_INITIATE			= 0x03E0,
			WM_DDE_TERMINATE,
			WM_DDE_ADVISE,
			WM_DDE_UNADVISE,
			WM_DDE_ACK,
			WM_DDE_DATA,
			WM_DDE_REQUEST,
			WM_DDE_POKE,
			WM_DDE_EXECUTE
		}

	
		private delegate void MessageEventHandler(object Sender, ref Message msg, ref bool Handled);

		private class NativeWindowWithMessages: System.Windows.Forms.NativeWindow
		{
			public event MessageEventHandler ProcessMessage;
			protected override void WndProc(ref Message m)
			{
				if (ProcessMessage!=null)
				{
					bool Handled=false;
					ProcessMessage(this,ref m,ref Handled);
					if (!Handled) base.WndProc(ref m);
				}
				else base.WndProc(ref m);
			}
		}

		private class DummyWindowWithMessages: NativeWindowWithMessages, IDisposable
		{
			public DummyWindowWithMessages()
			{
				CreateParams parms=new CreateParams();
				this.CreateHandle(parms);
			}
			public void Dispose()
			{
				if (this.Handle!=(IntPtr)0)
				{
					this.DestroyHandle();
				}
			}
		}

		#endregion
	
	}

	public delegate void DDEExecuteEventHandler(object Sender, string[] Commands);
}
