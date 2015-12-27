using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace iWay.RemoteControlBase.Utilities
{
    public static class SecurityUtils
    {
        private static MD5CryptoServiceProvider md5Computer = new MD5CryptoServiceProvider();
        private static SHA1CryptoServiceProvider sha1Computer = new SHA1CryptoServiceProvider();

        public static byte[] ComputeMD5(string s)
        {
            byte[] dat = Encoding.Unicode.GetBytes(s);
            return md5Computer.ComputeHash(dat);
        }

        public static byte[] ComputeMD5(string s, int startIndex, int count)
        {
            s = s.Substring(startIndex, count);
            byte[] dat = Encoding.Unicode.GetBytes(s);
            return md5Computer.ComputeHash(dat);
        }

        public static byte[] ComputeMD5(byte[] data)
        {
            return md5Computer.ComputeHash(data);
        }

        public static byte[] ComputeMD5(byte[] data, int startIndex, int count)
        {
            return md5Computer.ComputeHash(data, startIndex, count);
        }

        public static byte[] ComputeSHA1(string s)
        {
            byte[] dat = Encoding.Unicode.GetBytes(s);
            return sha1Computer.ComputeHash(dat);
        }

        public static byte[] ComputeSHA1(string s, int startIndex, int count)
        {
            s = s.Substring(startIndex, count);
            byte[] dat = Encoding.Unicode.GetBytes(s);
            return sha1Computer.ComputeHash(dat);
        }

        public static byte[] ComputeSHA1(byte[] data)
        {
            return sha1Computer.ComputeHash(data);
        }

        public static byte[] ComputeSHA1(byte[] data, int startIndex, int count)
        {
            return sha1Computer.ComputeHash(data, startIndex, count);
        }

        public static byte SingleByteCheck(byte[] data, int startIndex, int count)
        {
            byte r = 0;
            for (int i = 0; i < count; i++)
            {
                r ^= data[startIndex + i];
            }
            return r;
        }

        public static byte SingleByteCheck(byte[] data)
        {
            return SingleByteCheck(data, 0, data.Length);
        }

        public static bool SingleByteCheck(byte[] data, int startIndex, int count, byte checkCode)
        {
            byte c = SingleByteCheck(data, startIndex, count);
            return c == checkCode;
        }

        public static bool SingleByteCheck(byte[] data, byte checkCode)
        {
            return SingleByteCheck(data, 0, data.Length, checkCode);
        }

        public static void SimpleEncrypt(byte[] data, int startIndex, int count, byte[] code)
        {
            int m = startIndex; int mc = count;
            int n = 0; int nc = code.Length;
            while (m < mc)
            {
                data[m] += code[n];
                m++;
                n++;
                if (n >= nc)
                {
                    n = 0;
                }
            }
        }

        public static void SimpleEncrypt(byte[] data, byte[] code)
        {
            SimpleEncrypt(data, 0, data.Length, code);
        }

        public static void SimpleDecrypt(byte[] data, int startIndex, int count, byte[] code)
        {
            int m = startIndex; int mc = count;
            int n = 0; int nc = code.Length;
            while (m < mc)
            {
                data[m] -= code[n];
                m++;
                n++;
                if (n >= nc)
                {
                    n = 0;
                }
            }
        }
        
        public static void SimpleDecrypt(byte[] data, byte[] code)
        {
            SimpleDecrypt(data, 0, data.Length, code);
        }

        public static byte[] EncryptData(byte[] data, SymmetricAlgorithm encryptor)
        {
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Flush();
            byte[] encryptedData = memoryStream.ToArray();
            cryptoStream.Close();
            memoryStream.Close();
            return encryptedData;
        }

        public static byte[] DecryptData(byte[] data, SymmetricAlgorithm decryptor)
        {
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Flush();
            byte[] decryptedData = memoryStream.ToArray();
            cryptoStream.Close();
            memoryStream.Close();
            return decryptedData;
        }

    }
}
