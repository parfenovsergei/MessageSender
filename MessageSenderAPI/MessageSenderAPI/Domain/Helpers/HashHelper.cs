using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Cryptography;
using System.Text;

namespace MessageSenderAPI.Domain.Helpers
{
    public static class HashHelper
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[64];
            RandomNumberGenerator.Fill(salt);
            return BitConverter.ToString(salt).Replace("-", "").ToLower();
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(String.Concat(salt, password)));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
