using System;
using System.Text;
using System.IO;

namespace iWay.RemoteControlBase.Utilities
{
    public static class StreamUtils
    {
        public static void WriteBytes(Stream stream, byte[] data, int offset, int count)
        {
            stream.Write(data, offset, count);
        }

        public static void WriteBytes(Stream stream, byte[] data)
        {
            WriteBytes(stream, data, 0, data.Length);
        }

        public static byte[] ReadBytes(Stream stream, int count)
        {
            byte[] buffer = new byte[count];
            int cursor = 0;
            while (cursor != buffer.Length)
                cursor += stream.Read(buffer, cursor, buffer.Length - cursor);
            return buffer;
        }

        public static void WriteInt(Stream stream, int value)
        {
            byte[] dat = BitConverter.GetBytes(value);
            WriteBytes(stream, dat);
        }

        public static int ReadInt(Stream stream)
        {
            byte[] dat = ReadBytes(stream, sizeof(int));
            return BitConverter.ToInt32(dat, 0);
        }

        public static void WriteLong(Stream stream, long value)
        {
            byte[] dat = BitConverter.GetBytes(value);
            WriteBytes(stream, dat);
        }

        public static long ReadLong(Stream stream)
        {
            byte[] dat = ReadBytes(stream, sizeof(long));
            return BitConverter.ToInt64(dat, 0);
        }

        public static void WriteLong(Stream stream, float value)
        {
            byte[] dat = BitConverter.GetBytes(value);
            WriteBytes(stream, dat);
        }

        public static float ReadFloat(Stream stream)
        {
            byte[] dat = ReadBytes(stream, sizeof(float));
            return BitConverter.ToSingle(dat, 0);
        }

        public static void WriteLong(Stream stream, double value)
        {
            byte[] dat = BitConverter.GetBytes(value);
            WriteBytes(stream, dat);
        }

        public static double ReadDouble(Stream stream)
        {
            byte[] dat = ReadBytes(stream, sizeof(double));
            return BitConverter.ToDouble(dat, 0);
        }

        public static void WriteBlock(Stream stream, byte[] data)
        {
            WriteInt(stream, data.Length);
            WriteBytes(stream, data);
        }

        public static byte[] ReadBlock(Stream stream)
        {
            int length = ReadInt(stream);
            return ReadBytes(stream, length);
        }

        public static void FlushStream(Stream stream)
        {
            stream.Flush();
        }
    }
}
