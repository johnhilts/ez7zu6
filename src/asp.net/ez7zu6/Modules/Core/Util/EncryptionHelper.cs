using System.Security.Cryptography;
using ez7zu6.Core.Extensions;

namespace ez7zu6.Core.Util
{
    public class EncryptionHelper
    {
        public byte[] GenerateHash(string secretKey, string plainText)
        {
            var hash = new HMACSHA1(secretKey.GetBytes());
            hash.ComputeHash(plainText.GetBytes());

            return hash.Hash;
        }
    }
}
