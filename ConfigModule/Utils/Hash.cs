using System.Security.Cryptography;
using System.Text;

namespace ConfigModule.Utils
{
    public static class Hash
    {
        public static string GetSha256String(string str)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var hash = string.Empty;
                var sha256 = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(str));
                foreach (var theByte in sha256) hash += theByte.ToString("x2");

                return hash;
            }
        }
    }
}