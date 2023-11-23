using System.Text;
using System.Security.Cryptography;
using favorites.Services.Interfaces;

namespace favorites.Services
{
    public class HashService: IHashService
    {
        public string HashPassword(string password)
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
