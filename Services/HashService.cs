using System.Text;
using System.Security.Cryptography;

namespace favorites.Services
{
    public static class HashService
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
