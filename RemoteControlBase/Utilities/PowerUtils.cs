using System.Diagnostics;

namespace iWay.RemoteControlBase.Utilities
{
    public static class PowerUtils
    {
        public static void LogoffComputer()
        {
            UserDefinedOperation("-l");
        }

        public static void ShutdownComputer()
        {
            UserDefinedOperation("-s -t 0");
        }

        public static void HibernateComputer()
        {
            UserDefinedOperation("-h");
        }

        public static void RestartComputer()
        {
            UserDefinedOperation("-r -t 0");
        }

        public static void UserDefinedOperation(string shutdownArgs)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "shutdown.exe";
            info.Arguments = shutdownArgs;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            Process.Start(info);
        }
    }
}
