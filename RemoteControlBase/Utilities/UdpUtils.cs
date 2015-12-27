using System;
using System.Net;
using System.Net.Sockets;

namespace iWay.RemoteControlBase.Utilities
{
    public static class UdpUtils
    {
        public static Socket CreateReceiver(int port)
        {
            Socket socket = null;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Any, port));
                return socket;
            }
            catch (Exception ex)
            {
                if (socket != null)
                    socket.Close();
                throw ex;
            }
        } 
        
        public static Socket CreateSender()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
    }
}
