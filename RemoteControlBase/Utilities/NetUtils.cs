using System;
using System.Net;
using System.Net.NetworkInformation;

namespace iWay.RemoteControlBase.Utilities
{
    public static class NetUtils
    {
        public static string IPEndPointToString(IPEndPoint endPoint)
        {
            return endPoint.ToString();
        }

        public static IPEndPoint StringToIPEndPoint(string s)
        {
            string[] vals = s.Split(':');
            IPAddress ip = IPAddress.Parse(vals[0]);
            int port = int.Parse(vals[1]);
            return new IPEndPoint(ip, port);
        }

        public static bool IsIPv4AddressString(string host)
        {
            if (host == null || host == "")
                return false;
            string[] ip_split_strs = host.Split('.');
            foreach (string ip_split_str in ip_split_strs)
            {
                if (ip_split_str.Length < 1 || ip_split_str.Length > 3)
                    return false;
                foreach (char c in ip_split_str)
                    if (c < '0' || c > '9')
                        return false;
                int ip_split_val = int.Parse(ip_split_str);
                if (ip_split_val > 255 || ip_split_val < 0)
                    return false;
            }
            return true;
        }

        public static bool IsDomainName(string host)
        {
            foreach (char c in host)
            {
                if (c >= 'a' && c <= 'z')
                    continue;
                if (c >= 'A' && c <= 'Z')
                    continue;
                if (c >= '0' && c <= '9')
                    continue;
                if (c == '.' || c == '-')
                    continue;
                return false;
            }
            return true;
        }

        public static bool IsIPv4Address(IPAddress address)
        {
            return address.GetAddressBytes().Length == 4;
        }

        public static char GetIPAddressType(byte[] ipAddressData)
        {
            if (ipAddressData.Length != 4)
                return 'U';
            if (ipAddressData[0] >> 7 == 0)
                return 'A';
            if (ipAddressData[0] >> 6 == 2)
                return 'B';
            if (ipAddressData[0] >> 5 == 6)
                return 'C';
            if (ipAddressData[0] >> 4 == 16)
                return 'D';
            if (ipAddressData[0] >> 4 == 17)
                return 'E';
            return 'U';
        }

        public static IPAddress[] GetHostAddressList(string host = null)
        {
            string name = host == null ? Dns.GetHostName() : null;
            IPHostEntry entry = Dns.GetHostEntry(name);
            return entry.AddressList;
        }

        public static IPAddress GetPublicIPAddress(params IPAddress[] addressList)
        {
            foreach (IPAddress ip in addressList)
                if (!IsLocalIPAddress(ip))
                    return ip;
            return null;
        }

        public static IPAddress GetLocalIPAddress(params IPAddress[] addressList)
        {
            foreach (IPAddress ip in addressList)
                if (IsLocalIPAddress(ip))
                    return ip;
            return null;
        }

        public static bool IsPublicIPAddress(IPAddress address)
        {
            return !IsLocalIPAddress(address);
        }

        public static bool IsLocalIPAddress(IPAddress address)
        {
            byte[] ip_dat = address.GetAddressBytes();
            if (ip_dat.Length != 4)
                return false;
            if (ip_dat[0] == 10)
                return true;
            if (ip_dat[0] == 172 && ip_dat[1] >= 16 && ip_dat[1] <= 31)
                return true;
            if (ip_dat[0] == 192 && ip_dat[1] == 168)
                return true;
            return false;
        }

        public static bool IsInternetConnected(int checkTimeout)
        {
            if (checkTimeout < 250)
                throw new Exception("Timeout >= 250.");
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds <= checkTimeout)
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply rep = ping.Send("110.75.216.92", 250);
                    ping.Dispose();
                    if (rep.Status == IPStatus.Success)
                        return true;
                }
                catch { }
            }
            return false;
        }
    }
}
