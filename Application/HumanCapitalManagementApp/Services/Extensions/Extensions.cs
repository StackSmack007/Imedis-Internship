using System.Security.Cryptography;
using System.Text;

namespace Services.Extensions
{
    public static class Extensions
    {
        public static string ToHashedString(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
        private static byte[] GetHash( string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }



    }
}
