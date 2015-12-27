using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace iWay.RemoteControlBase.Utilities
{
    public static class TcpUtils
    {
        public static Socket CreateServer(int port, int backlog)
        {
            Socket socket = null;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Any, port));
                socket.Listen(backlog);
                return socket;
            }
            catch (Exception ex)
            {
                if (socket != null)
                    socket.Close();
                throw ex;
            }
        }

        public static Socket CreateServer(ref int randomPort, int backlog)
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 16; i++)
            {
                Socket socket = null;
                try
                {
                    int textPort = r.Next(1024, 65535);
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, textPort));
                    socket.Listen(backlog);
                    randomPort = textPort;
                    return socket;
                }
                catch
                {
                    if (socket != null)
                        socket.Close();
                    continue;
                }
            }
            return null;
        }

        public static Socket CreateClient()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static Socket CreateClient(IPEndPoint remoteEndPoint)
        {
            Socket socket = null;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEndPoint);
                return socket;
            }
            catch (Exception ex)
            {
                if (socket != null)
                    socket.Close();
                throw ex;
            }
        }

        public static void DestroyServer(Socket socket)
        {
            if (socket != null)
                socket.Close();
        }

        public static void DestroyServer(Socket socket, int timeout)
        {
            Thread.Sleep(timeout);
            DestroyServer(socket);
        }

        public static void DestroyClient(Socket socket)
        {
            if (socket != null)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                }
                catch { }
                socket.Close();
            }
        }

        public static void DestroyClient(Socket socket, byte[] dataToSend, int timeout)
        {
            if (socket != null)
            {
                try
                {
                    socket.Send(dataToSend);
                    Thread.Sleep(timeout);
                }
                catch { }
            }
            DestroyClient(socket);
        }
    }
}
