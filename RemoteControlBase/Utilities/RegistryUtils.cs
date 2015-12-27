using System;
using Microsoft.Win32;

namespace iWay.RemoteControlBase.Utilities
{
    public static class RegistryUtils
    {
        public static bool GetIsStartupProgram(string name, string value)
        {
            RegistryKey key_start = OpenSubKey("CurrentUser", @"Software\Microsoft\Windows\CurrentVersion\Run");
            string[] names = key_start.GetValueNames();
            bool result = Array.IndexOf(names, name) > -1 && key_start.GetValue(name).ToString() == value;
            key_start.Close();
            return result;
        }

        public static void SetAsStartupProgram(string name, string value, bool isStartup)
        {
            RegistryKey key_start = OpenSubKey("CurrentUser", @"Software\Microsoft\Windows\CurrentVersion\Run", true);
            string[] names = key_start.GetValueNames();
            if (isStartup)
                key_start.SetValue(name, value);
            else
                if (Array.IndexOf(names, name) > -1 && key_start.GetValue(name).ToString() == value)
                    key_start.DeleteValue(name);
            key_start.Close();
        }

        public static RegistryKey OpenSubKey(string entry, string keyPath, bool writable = false)
        {
            switch (entry)
            {
                case "ClassesRoot":
                    return Registry.ClassesRoot.OpenSubKey(keyPath, writable);
                case "CurrentUser":
                    return Registry.CurrentUser.OpenSubKey(keyPath, writable);
                case "LocalMachine":
                    return Registry.LocalMachine.OpenSubKey(keyPath, writable);
                case "Users":
                    return Registry.Users.OpenSubKey(keyPath, writable);
                case "CurrentConfig":
                    return Registry.CurrentConfig.OpenSubKey(keyPath, writable);
                case "PerformanceData":
                    return Registry.PerformanceData.OpenSubKey(keyPath, writable);
                default:
                    throw new Exception("Entry : " + entry + " not found.");
            }
        }

        public static object ReadRegistry(string entry, string keyPath, string name)
        {
            RegistryKey reg_key = OpenSubKey(entry, keyPath);
            object value = reg_key.GetValue(name);
            reg_key.Close();
            return value;
        }

        public static void WriteRegistry(string entry, string keyPath, string name, object value)
        {
            RegistryKey reg_key = OpenSubKey(entry, keyPath, true);
            reg_key.SetValue(name, value);
            reg_key.Close();
        }
    }
}
