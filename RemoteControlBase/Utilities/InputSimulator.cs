using System;
using System.Windows.Forms;

namespace iWay.RemoteControlBase.Utilities
{
    public static class InputSimulator
    {
        public static void GetCursorPosition(ref int x, ref int y)
        {
            long position = 0;
            WinAPIUtils.GetCursorPos(ref position);
            x = (int)(position << 32 >> 32);
            y = (int)(position >> 32);
        }

        public static void SetCursorPosition(int x, int y)
        {
            WinAPIUtils.SetCursorPos(x, y);
        }

        public static void CreateMouseDown(int button)
        {
            switch (button)
            {
                case 0:
                    WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    break;
                case 1:
                    WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    break;
                case 2:
                    WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    break;
                default:
                    throw new Exception("Button can only be 0 ~ 2.");
            }
        }

        public static void CreateMouseDown(int button, int x, int y)
        {
            SetCursorPosition(x, y);
            CreateMouseDown(button);
        }

        public static void CreateMouseUp(int button)
        {
            switch (button)
            {
                case 0:
                    WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case 1:
                    WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                    break;
                case 2:
                    WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                default:
                    throw new Exception("Button can only be 0 ~ 2.");
            }
        }

        public static void CreateMouseUp(int x, int y, int button)
        {
            SetCursorPosition(x, y);
            CreateMouseUp(button);
        }

        public static void CreateMouseClick(int button, int times)
        {
            switch (button)
            {
                case 0:
                    while (times-- > 0)
                        WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_LEFTDOWN | WinAPIUtils.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case 1:
                    while (times-- > 0)
                        WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_MIDDLEDOWN | WinAPIUtils.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                    break;
                case 2:
                    while (times-- > 0)
                        WinAPIUtils.mouse_event(WinAPIUtils.MOUSEEVENTF_RIGHTDOWN | WinAPIUtils.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                default:
                    throw new Exception("Button can only be 0 ~ 2.");
            }
        }

        public static void CreateMouseClick(int x, int y, int button, int times)
        {
            SetCursorPosition(x, y);
            CreateMouseClick(button, times);
        }

        public static void CreateKeyboardDown(byte virtualkey)
        {
            WinAPIUtils.keybd_event(virtualkey, 0, 0, 0);
        }

        public static void CreateKeyboardDown(Keys keys)
        {
            CreateKeyboardDown((byte)keys);
        }

        public static void CreateKeyboardUp(byte virtualkey)
        {
            WinAPIUtils.keybd_event(virtualkey, 0, 2, 0);
        }

        public static void CreateKeyboardUp(Keys keys)
        {
            CreateKeyboardUp((byte)keys);
        }

        public static void CreateKeyboardPress(bool shift, bool ctrl, bool alt, byte virtualkey)
        {
            if (shift)
                WinAPIUtils.keybd_event(16, 0, 0, 0);
            if (ctrl)
                WinAPIUtils.keybd_event(17, 0, 0, 0);
            if (alt)
                WinAPIUtils.keybd_event(18, 0, 0, 0);

            WinAPIUtils.keybd_event(virtualkey, 0, 0, 0);
            WinAPIUtils.keybd_event(virtualkey, 0, 2, 0);

            if (shift)
                WinAPIUtils.keybd_event(16, 0, 2, 0);
            if (ctrl)
                WinAPIUtils.keybd_event(17, 0, 2, 0);
            if (alt)
                WinAPIUtils.keybd_event(18, 0, 2, 0);
        }

        public static void CreateKeyboardPress(bool shift, bool ctrl, bool alt, Keys key)
        {
            byte virtualkey = (byte)key;
            CreateKeyboardPress(shift, ctrl, alt, virtualkey);
        }

    }
}
