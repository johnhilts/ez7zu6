using System.Text;

namespace ez7zu6.Core.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
