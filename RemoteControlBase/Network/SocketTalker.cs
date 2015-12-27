using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Threading;

namespace iWay.RemoteControlBase.Network.SocketTalker
{
    public class SocketTalker
    {
        private Socket mSocket;
        private JavaScriptSerializer mSerializer;
        private ICryptoTransform mEncryptor;
        private ICryptoTransform mDecryptor;

        public SocketTalker(Socket socket)
        {
            mSocket = socket;
            mSerializer = new JavaScriptSerializer();
        }

        public SocketTalker(Socket socket, ICryptoTransform encryptor, ICryptoTransform decryptor)
            : this(socket)
        {
            mEncryptor = encryptor;
            mDecryptor = decryptor;
        }

        public Socket Socket
        {
            get
            {
                return mSocket;
            }
 
        }

        public void SendData(byte[] value)
        {
            mSocket.Send(value, 0, value.Length, SocketFlags.None);
        }

        public void SendData(byte[] value, int startIndex, int count)
        {
            mSocket.Send(value, startIndex, count, SocketFlags.None);
        }

        public void SendInt(int value)
        {
            byte[] valueData = BitConverter.GetBytes(value);
            SendData(valueData);
        }

        public void SendLong(long value)
        {
            byte[] valueData = BitConverter.GetBytes(value);
            SendData(valueData);
        }

        public void SendFloat(float value)
        {
            byte[] valueData = BitConverter.GetBytes(value);
            SendData(valueData);
        }

        public void SendDouble(double value)
        {
            byte[] valueData = BitConverter.GetBytes(value);
            SendData(valueData);
        }

        public void SendPacket(byte[] value)
        {
            if (mEncryptor != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, mEncryptor, CryptoStreamMode.Write);
                cryptoStream.Write(value, 0, value.Length);
                if (cryptoStream.HasFlushedFinalBlock == false)
                {
                    cryptoStream.FlushFinalBlock();
                }

                value = memoryStream.ToArray();

                cryptoStream.Close();
                memoryStream.Close();
            }
            SendInt(value.Length);
            SendData(value);
        }

        public void SendString(string value)
        {
            byte[] valueData = Encoding.UTF8.GetBytes(value);
            SendPacket(valueData);
        }

        public void SendObject(object value)
        {
            string valueData = mSerializer.Serialize(value);
            SendString(valueData);
        }

        public byte[] ReceiveData(int count)
        {
            byte[] data = new byte[count];
            int cursor = 0;
            int tryTimes = 0;
            while (cursor != count)
            {
                if (mSocket.Connected)
                {
                    int receivedDataCount = mSocket.Receive(data, cursor, count - cursor, SocketFlags.None);
                    cursor += receivedDataCount;
                    if (receivedDataCount < 0)
                    {
                        throw new Exception("ReceiveData failed, Always received " + receivedDataCount + " sized data.");
                    }
                    if (receivedDataCount == 0)
                    {
                        tryTimes++;
                        if (tryTimes > 8)
                        {
                            throw new Exception("ReceiveData failed, Always received zero sized data.");
                        }
                        Thread.Sleep(1);
                    }
                    else
                    {
                        tryTimes = 0;
                    }
                }
                else
                {
                    throw new Exception("ReceiveData failed, The socket is not connected.");
                }
            }
            return data;
        }

        public int ReceiveInt()
        {
            byte[] data = ReceiveData(sizeof(int));
            return BitConverter.ToInt32(data, 0);
        }

        public long ReceiveLong()
        {
            byte[] data = ReceiveData(sizeof(long));
            return BitConverter.ToInt64(data, 0);
        }

        public float ReceiveFloat()
        {
            byte[] data = ReceiveData(sizeof(float));
            return BitConverter.ToSingle(data, 0);
        }

        public double ReceiveDouble()
        {
            byte[] data = ReceiveData(sizeof(double));
            return BitConverter.ToDouble(data, 0);
        }

        public byte[] ReceivePacket()
        {
            int length = ReceiveInt();
            byte[] value = ReceiveData(length);
            if (mDecryptor != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, mDecryptor, CryptoStreamMode.Write);
                cryptoStream.Write(value, 0, value.Length);
                if (cryptoStream.HasFlushedFinalBlock == false)
                {
                    cryptoStream.FlushFinalBlock();
                }

                value = memoryStream.ToArray();

                cryptoStream.Close();
                memoryStream.Close();
            }
            return value;
        }

        public string ReceiveString()
        {
            byte[] data = ReceivePacket();
            return Encoding.UTF8.GetString(data);
        }

        public T ReceiveObject<T>()
        {
            string data = ReceiveString();
            return mSerializer.Deserialize<T>(data);
        }

        private object mCloseLock = new object();
        private bool mIsClosed = false;

        public bool IsClosed
        {
            get
            {
                return mIsClosed;
            }
        }

        public void Close()
        {
            lock (mCloseLock)
            {
                if (mIsClosed)
                {
                    return;
                }
                mSocket.Close();
                mIsClosed = true;
            }
        }
    }
}
