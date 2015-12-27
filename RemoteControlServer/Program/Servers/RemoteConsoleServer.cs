using System;
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace iWay.RemoteControlServer.Program.Servers
{
    public class RemoteConsoleServer : RCServer
    {
        public RemoteConsoleServer(Socket socket, TripleDESCryptoServiceProvider tdesProvider)
            : base(socket, tdesProvider)
        {
        }

        private StringBuilder mDataBuilder;
        private Process mCmdProcess;
        private Thread mInputWriter;
        private Thread mOutputReader;
        private Thread mErrorReader;
        private Thread mDataSender;

        public override void BeginService()
        {
            mDataBuilder = new StringBuilder();

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.WorkingDirectory = Environment.SystemDirectory;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            mCmdProcess = Process.Start(info);

            mInputWriter = new Thread(WriteInput);
            mInputWriter.IsBackground = true;
            mInputWriter.Start();
            mOutputReader = new Thread(ReadOutput);
            mOutputReader.IsBackground = true;
            mOutputReader.Start();
            mErrorReader = new Thread(ReadError);
            mErrorReader.IsBackground = true;
            mErrorReader.Start();
            mDataSender = new Thread(SendData);
            mDataSender.IsBackground = true;
            mDataSender.Start();
        }

        private void WriteInput()
        {
            try
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    string input = mSocketTalker.ReceiveString();
                    mCmdProcess.StandardInput.Write(input);
                }
            }
            catch
            {
                Close();
            }
        }

        private void ReadOutput()
        {
            try
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    char singleChar = (char) mCmdProcess.StandardOutput.Read();
                    lock (mDataBuilder)
                    {
                        mDataBuilder.Append(singleChar);
                    }
                }
            }
            catch
            {
                Close();
            }
        }

        private void ReadError()
        {
            try
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    char singleChar = (char)mCmdProcess.StandardError.Read();
                    lock (mDataBuilder)
                    {
                        mDataBuilder.Append(singleChar);
                    }
                }
            }
            catch
            {
                Close();
            }
        }

        private void SendData()
        {
            try
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    bool zeroSizedData = false;
                    lock (mDataBuilder)
                    {
                        if (mDataBuilder.Length > 0)
                        {
                            string dataString = mDataBuilder.ToString();
                            mSocketTalker.SendString(dataString);
                            mDataBuilder.Clear();
                        }
                        else
                        {
                            zeroSizedData = true;
                        }
                    }
                    if (zeroSizedData)
                    {
                        Thread.Sleep(128);
                    }
                }
            }
            catch
            {
                Close();
            }
        }

        protected override void CloseCreatedResources()
        {
            mCmdProcess.Kill();
            mCmdProcess.Close();
        }
    }
}
