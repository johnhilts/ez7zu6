using System.Text;

namespace ez7zu6.Core.Extensions
{
    public static class ByteExtensions
    {
        public static string GetHexStringFromBytes(this byte[] bytes)
        {
            if (bytes == null)
                return null;

            var hexString = new StringBuilder(50);
            foreach (byte b in bytes)
            {
                hexString.Append(b.ToString("x2"));
            }

            return hexString.ToString();
        }
    }
}
