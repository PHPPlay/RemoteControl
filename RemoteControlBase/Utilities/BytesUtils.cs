using System;

namespace iWay.RemoteControlBase.Utilities
{
    public static class BytesUtils
    {
        public static byte[] GetRange(byte[] data, int startIndex, int count)
        {
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i++)
                buffer[i] = data[startIndex + i];
            return buffer;
        }

        public static byte[] GetRange(byte[] data, int startIndex)
        {
            return GetRange(data, startIndex, data.Length - startIndex);
        }

        public static void SetRange(byte[] dst, int dstOffset, byte[] src, int srcOffset, int count)
        {
            for (int i = 0; i < count; i++)
                dst[dstOffset + i] = src[srcOffset + i];
        }

        public static void SetRange(byte[] dst, int dstOffset, byte[] src)
        {
            SetRange(dst, dstOffset, src, 0, src.Length);
        }

        public static void SetRange(byte[] dst, int dstOffset, byte value, int count)
        {
            int ptr = dstOffset;
            int end = dstOffset + count;
            while (ptr < end) 
                dst[ptr++] = value;
        }

        public static byte[] PadLeft(byte[] data, byte value, int totalLength)
        {
            if (data.Length >= totalLength)
                return data;
            byte[] buffer = new byte[totalLength];
            SetRange(data, 0, value, totalLength - data.Length);
            SetRange(buffer, data.Length, data);
            return buffer;
        }

        public static byte[] PadRight(byte[] data, byte value, int totalLength)
        {
            if (data.Length >= totalLength)
                return data;
            byte[] buffer = new byte[totalLength];
            SetRange(buffer, data.Length, data);
            SetRange(data, data.Length, value, totalLength - data.Length);
            return buffer;
        }

        public static byte[] Merge(params byte[][] dataSets)
        {
            int totalLength = 0;
            foreach (byte[] set in dataSets)
                totalLength += set.Length;
            byte[] buffer = new byte[totalLength];
            for (int i = 0, offset = 0; i < dataSets.Length; i++)
            {
                SetRange(buffer, offset, dataSets[i]);
                offset += dataSets[i].Length;
            }
            return buffer;
        }

        public static byte[] Random(int length)
        {
            byte[] dat = new byte[length];
            Random r = new Random();
            r.NextBytes(dat);
            return dat;
        }

        public static byte[] Insert(byte[] dst, int dstOffset, byte[] src, int srcOffset, int count)
        {
            byte[] buf = new byte[count + dst.Length];
            SetRange(buf, 0, dst, 0, dstOffset);
            SetRange(buf, dstOffset, src, srcOffset, count);
            SetRange(buf, dstOffset + count, dst, dstOffset, dst.Length - dstOffset);
            return buf;
        }

        public static byte[] Insert(byte[] dst, int dstOffset, byte[] src)
        {
            return Insert(dst, dstOffset, src, 0, src.Length);
        }

        public static byte[] Insert(byte[] dst, int dstOffset, byte value, int count)
        {
            byte[] buf = new byte[count + dst.Length];
            SetRange(buf, 0, dst, 0, dstOffset);
            SetRange(buf, dstOffset, value, count);
            SetRange(buf, dstOffset + count, dst, dstOffset, dst.Length - dstOffset);
            return buf;
        }

        public static void Reverse(byte[] data, int startIndex, int count)
        {
            int num1 = startIndex;
            int num2 = startIndex + count - 1;
            while (num1 < num2)
            {
                byte b = data[num2];
                data[num2] = data[num1];
                data[num1] = b;
                num1++;
                num2--;
            }
        }

        public static void Reverse(byte[] data)
        {
            Reverse(data, 0, data.Length);
        }

        public static bool IsEqual(byte[] a, int aStart, byte[] b, int bStart, int count)
        {
            if (aStart + count > a.Length || bStart + count > b.Length)
                return false;
            for (int i = 0; i < count; i++)
                if (a[aStart + i] != b[bStart + i])
                    return false;
            return true;
        }

        public static bool IsEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }
    }
}
