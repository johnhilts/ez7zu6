using System;
using System.Text;

namespace ez7zu6.Core.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        // TODO: do we need this?? we'll know for sure once we implement the pwd check ...
        public static byte[] GetBytesFromHexString(this string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
                return null;

            var bytes = new byte[hexString.Length / 2];
            int strIndex;
            int byteIndex = 0;

            for (strIndex = 0; strIndex < hexString.Length; strIndex += 2)
            {
                string hexNumber = hexString.Substring(strIndex, 2);
                bytes[byteIndex] = Convert.ToByte(hexNumber, 16);
                byteIndex++;
            }

            return bytes;
        }
    }
}
