using System;
using System.Collections.Generic;
using System.IO;

namespace iWay.RemoteControlBase.Utilities
{
    public static class LogUtils
    {
        private static List<string> logs = new List<string>();

        public static void AddLog(string message, string timeFormat = null)
        {
            lock (logs)
            {
                if (timeFormat == null)
                    logs.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message);
                else
                    logs.Add(DateTime.Now.ToString(timeFormat) + " " + message);
            }
        }

        public static List<string> GetAllLogs()
        {
            return logs;
        }

        public static string GetFirstLog()
        {
            if (logs.Count > 0)
                return logs[0];
            return null;
        }

        public static string GetLastLog()
        {
            if (logs.Count > 0)
                return logs[logs.Count - 1];
            return null;
        }

        public static int GetLogsCount()
        {
            return logs.Count;
        }

        public static void ClearLogs()
        {
            lock (logs)
                logs.Clear();
        }

        public static void SaveToFile(string filePath, bool appendFile = true)
        {
            if (logs.Count == 0)
                return;
            if (appendFile)
                File.AppendAllLines(filePath, logs);
            else
                File.WriteAllLines(filePath, logs);
        }
    }
}
