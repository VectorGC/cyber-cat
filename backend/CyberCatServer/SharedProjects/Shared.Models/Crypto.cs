using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public static class Crypto
    {
        private static readonly byte[] IV =
        {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };

        private static byte[] DeriveKeyFromPassword(string password, string salt)
        {
            var saltBytes = Encoding.Unicode.GetBytes(salt);
            using (var rfc = new Rfc2898DeriveBytes(password, saltBytes))
            {
                return rfc.GetBytes(32);
            }
        }

        public static async Task<string> EncryptAsync(string original, string passphrase, string salt = "cyber")
        {
            using (var aes = Aes.Create())
            {
                aes.Key = DeriveKeyFromPassword(passphrase, salt);
                aes.IV = IV;
                using (var output = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(output, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                        await writer.WriteAsync(original);

                    var bytes = output.ToArray();
                    return Convert.ToBase64String(bytes);
                }
            }
        }

        public static async Task<string> DecryptAsync(string encryptedString, string passphrase, string salt = "cyber")
        {
            var encrypted = Convert.FromBase64String(encryptedString);

            using (var aes = Aes.Create())
            {
                aes.Key = DeriveKeyFromPassword(passphrase, salt);
                aes.IV = IV;
                using (var input = new MemoryStream(encrypted))
                {
                    using (var cryptoStream = new CryptoStream(input, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cryptoStream))
                    {
                        var original = await reader.ReadToEndAsync();
                        return original;
                    }
                }
            }
        }
    }
}