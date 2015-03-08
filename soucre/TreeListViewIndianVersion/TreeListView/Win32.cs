using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

namespace Win32
{
   //#define KEYDOWN(vk_code)  ((GetAsyncKeyState(vk_code) & 0x8000) ? 1 : 0)
   //#define KEYUP(vk_code)  ((GetAsyncKeyState(vk_code) & 0x8000) ? 0 : 1)

   public enum VirtualKeyMappingOption
   {
      MAPVK_VK_TO_VSC = 0x00,
      MAPVK_VSC_TO_VK = 0x01,
      MAPVK_VK_TO_CHAR = 0x02,
      MAPVK_VSC_TO_VK_EX = 0x03,
      MAPVK_VK_TO_VSC_EX = 0x04
   }

   static class Helpers
   {
      public static Point ToPoint(int lParam)
      {
         int x = GetLowWord(lParam);
         int y = GetHighWord(lParam);
         return new Point(x, y);
      }

      public static int GetHighWord(int intValue)
      {
         return (intValue >> 16) & 0xFFFF;
      }

      public static int GetLowWord(int intValue)
      {
         return (intValue & 0xFFFF);
      }

      public static short GetHighByte(short shortValue)
      {
         return (short)((shortValue >> 8) & 0xFF);
      }

      public static short GetLowByte(short shortValue)
      {
         return (short)(shortValue & 0xFF);
      }

      public static PInvoke.TEXTMETRIC GetTextMetrics(Graphics g)
      {
         Debug.Assert(g != null, "The specified graphics is NULL!");

         bool holdingHdc = false;
         var tm = new PInvoke.TEXTMETRIC();
         
         // GC.KeepAlive(tm);
         var tmHandle = GCHandle.Alloc(tm, GCHandleType.Pinned);

         try
         {
            IntPtr hdc = g.GetHdc();
            holdingHdc = true;

            bool bOk = PInvoke.GetTextMetrics(hdc, out tm);
            Debug.Assert(bOk, "GetTextMetrics failed. ErrorCode: " + PInvoke.GetLastError());
         }
         catch
         {
            Debug.Assert(false);
         }
         finally
         {
            if (holdingHdc)
            {
               g.ReleaseHdc();
            }

            if (tmHandle.IsAllocated)
            {
               tmHandle.Free();
            }
         }

         return tm;
      }
   }

   public static class PInvoke
   {
      [DllImport("Kernel32.dll")]
      public static extern uint GetLastError();

      [DllImport("user32.dll")]
      public static extern uint MapVirtualKey(uint uCode, uint uMapType);

      [DllImport("user32.dll")]
      public static extern short GetKeyState(int virtualKey);

      [StructLayout(LayoutKind.Sequential, Pack = 1)]
      public struct TEXTMETRIC
      {
         public int tmHeight;
         public int tmAscent;
         public int tmDescent;
         public int tmInternalLeading;
         public int tmExternalLeading;
         public int tmAveCharWidth;
         public int tmMaxCharWidth;
         public int tmWeight;
         public int tmOverhang;
         public int tmDigitizedAspectX;
         public int tmDigitizedAspectY;
         
         [MarshalAs(UnmanagedType.I2)]
         public char tmFirstChar;

         [MarshalAs(UnmanagedType.I2)]
         public char tmLastChar;

         [MarshalAs(UnmanagedType.I2)]
         public char tmDefaultChar;

         [MarshalAs(UnmanagedType.I2)]
         public char tmBreakChar;

         public byte tmItalic;
         public byte tmUnderlined;
         public byte tmStruckOut;
         public byte tmPitchAndFamily;
         public byte tmCharSet;
      }

      [DllImport("Gdi32.dll", CharSet = CharSet.Unicode)]
      public static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRIC lptm);

      [DllImport("Gdi32.dll", CharSet = CharSet.Unicode)]
      public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

      [DllImport("Gdi32.dll", CharSet = CharSet.Unicode)]
      public static extern bool DeleteObject(IntPtr hdc);
   }

   public enum VirtualKeyCodes
   {
      VK_BACK = 0x08,
      VK_TAB = 0x09,

      VK_SHIFT = 0x10,
      VK_CONTROL = 0x11,
      VK_ALT = 0x12,
      VK_MENU = 0x12,
      VK_PAUSE = 0x13,
      VK_CAPITAL = 0x14,

      VK_LEFT = 0x25,
      VK_UP = 0x26,
      VK_RIGHT = 0x27,
      VK_DOWN = 0x28,

      VK_INSERT = 0x2D,
      VK_DELETE = 0x2E,
      VK_HELP = 0x2F
   }

   public static class Messages
   {
      public const int WM_KEYFIRST = 0x0100;
      public const int WM_KEYDOWN = 0x0100;
      public const int WM_KEYUP = 0x0101;
      public const int WM_CHAR = 0x0102;
      public const int WM_DEADCHAR = 0x0103;
      public const int WM_SYSKEYDOWN = 0x0104;
      public const int WM_SYSKEYUP = 0x0105;
      public const int WM_SYSCHAR = 0x0106;
      public const int WM_SYSDEADCHAR = 0x0107;
      //#if(_WIN32_WINNT >= 0x0501)
      //public const int WM_UNICHAR  = 0x0109
      //public const int WM_KEYLAST  = 0x0109
      //public const int UNICODE_NOCHAR = 0xFFFF
      //#else
      //public const int WM_KEYLAST  = 0x0108
      //#endif

      public const int WM_TIMER = 0x0113;
      public const int WM_HSCROLL = 0x0114;
      public const int WM_VSCROLL = 0x0115;

      public const int WM_CTLCOLORMSGBOX = 0x0132;
      public const int WM_CTLCOLOREDIT = 0x0133;
      public const int WM_CTLCOLORLISTBOX = 0x0134;
      public const int WM_CTLCOLORBTN = 0x0135;
      public const int WM_CTLCOLORDLG = 0x0136;
      public const int WM_CTLCOLORSCROLLBAR = 0x0137;
      public const int WM_CTLCOLORSTATIC = 0x0138;
      public const int MN_GETHMENU = 0x01E1;

      public const int WM_MOUSEFIRST = 0x0200;
      public const int WM_MOUSEMOVE = 0x0200;
      public const int WM_LBUTTONDOWN = 0x0201;
      public const int WM_LBUTTONUP = 0x0202;
      public const int WM_LBUTTONDBLCLK = 0x0203;
      public const int WM_RBUTTONDOWN = 0x0204;
      public const int WM_RBUTTONUP = 0x0205;
      public const int WM_RBUTTONDBLCLK = 0x0206;
      public const int WM_MBUTTONDOWN = 0x0207;
      public const int WM_MBUTTONUP = 0x0208;
      public const int WM_MBUTTONDBLCLK = 0x0209;

      // #if (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400)
      public const int WM_MOUSEWHEEL = 0x020A;

      // #if (_WIN32_WINNT >= 0x0500)
      public const int WM_XBUTTONDOWN = 0x020B;
      public const int WM_XBUTTONUP = 0x020C;
      public const int WM_XBUTTONDBLCLK = 0x020D;

      // #if (_WIN32_WINNT >= 0x0600)
      public const int WM_MOUSEHWHEEL = 0x020E;

      // #if (_WIN32_WINNT >= 0x0600)
      public const int WM_MOUSELAST = 0x020E;
      //#elif (_WIN32_WINNT >= 0x0500)
      //public const int WM_MOUSELAST =      0x020D
      //#elif (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400)
      //public const int WM_MOUSELAST =      0x020A
      //#else
      //public const int WM_MOUSELAST =      0x0209*/

      public const int LVM_SETCOLUMNORDERARRAY = 0x1000 + 58;
      public const int LVM_REDRAWITEMS = 0x1000 + 21;
   }
}