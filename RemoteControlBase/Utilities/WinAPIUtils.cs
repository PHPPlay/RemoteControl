using System;
using System.Runtime.InteropServices;

namespace iWay.RemoteControlBase.Utilities
{
    public static class WinAPIUtils
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(ref long x_y);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetCursorPos(int x, int y);

        public const int MOUSEEVENTF_MOVE = 0x0001;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const byte VK_SHIFT = 16;
        public const byte VK_CONTROL = 17;
        public const byte VK_MENU = 18;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void LockWorkStation();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr hwnd, string lpszText, string lpszCaption, uint nType);

        public struct AeroMargins
        {
            public AeroMargins(int l, int t, int r, int b)
            {
                left = l;
                top = t;
                right = r;
                bottom = b;
            }

            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref AeroMargins margins);
    }
}
