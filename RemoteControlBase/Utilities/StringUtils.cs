using System;
using System.Collections.Generic;
using System.Text;

namespace iWay.RemoteControlBase.Utilities
{
    public static class StringUtils
    {       
        public static string[] SplitString(string s, params char[] separator)
        {
            return s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitString(string s, params string[] separator)
        {
            return s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string MergeStrings(char separator, params string[] strings)
        {
            return MergeStrings(separator.ToString(), strings);
        }

        public static string MergeStrings(string separator, IEnumerable<string> strings)
        {       
            StringBuilder sb = new StringBuilder();
            int ptr = 0;
            foreach (string str in strings)
            {
                if (ptr > 0)
                    sb.Append(separator);
                sb.Append(str);
                ptr++;
            }
            return sb.ToString();
        }

        public static bool IsNumeric(char c)
        {
            return (c >= '0' && c <= '9') || c == '.';
        }

        public static bool IsNumeric(string s)
        {
            if (s == null || s == "")
                return false;
            int dotcount = 0;
            foreach (char c in s)
            {
                if (!IsNumeric(c))
                    return false;
                if (c == '.')
                    dotcount++;
            }
            return dotcount <= 1;
        }

        public static string GetNumberFromIndex(string s, int index)
        {
            return GetNumberFromIndex(s, index, 1);
        }

        public static string GetNumberFromIndex(string s, int index, int maxDotCount)
        {
            StringBuilder builer = new StringBuilder();
            int dotcount = 0;
            while (index < s.Length)
            {
                char c = s[index];
                if (c == '.')
                    dotcount++;
                if (dotcount == maxDotCount + 1)
                    break;
                if (IsNumeric(c))
                    builer.Append(c);
                else
                    break;
                index++;
            }
            if (builer.Length == 0)
                return "";
            if (builer[builer.Length - 1] == '.')
                builer.Append('0');
            if (builer[0] == '.')
                builer.Insert(0, '0');
            return builer.ToString();
        }

        public static string GetNumberAfterChar(string s, char character)
        {
            int index = s.IndexOf(character);
            if(index == -1)
                throw new Exception("Not found the char.");
            return GetNumberFromIndex(s, index + 1);
        }

        public static string GetNumberAfterChar(string s, char character, int charCount)
        {
            int cnt = 0;
            int idx = 0;
            while (idx < s.Length)
            {
                if (s[idx] == character)
                    cnt++;
                if (cnt == charCount)
                    break;
                idx++;
            }
            if (cnt != charCount)
                throw new Exception("Not found so many chars.");
            return GetNumberFromIndex(s, idx + 1);
        }

        public static byte[] FromHex(string s)
        {
            if (s == null)
                return null;
            if (s == "")
                return new byte[0];
            s = s.ToUpper().Replace("-", "");
            byte[] r = new byte[s.Length / 2];
            for (int i = 0; i < r.Length; i++)
            {
                byte a = 0;
                byte b = 0;
                char ac = s[i * 2];
                char bc = s[i * 2 + 1];
                if (ac >= '0' && ac <= '9')
                    a = (byte)(ac - '0');
                else if (ac >= 'A' && ac <= 'F')
                    a = (byte)(ac - 'A' + 10);
                else
                    throw new Exception("Hex string error.");
                if (bc >= '0' && bc <= '9')
                    b = (byte)(bc - '0');
                else if (bc >= 'A' && bc <= 'F')
                    b = (byte)(bc - 'A' + 10);
                else
                    throw new Exception("Hex string error.");
                r[i] = (byte)((a << 4) | b);
            }
            return r;
        }

        public static string ToHex(byte[] data, int startIndex, int count)
        {
            return BitConverter.ToString(data, startIndex, count);
        }

        public static string ToHex(byte[] data)
        {
            return BitConverter.ToString(data);
        }
    }
}
