using System.Security.Cryptography;
using System.Threading.Tasks;
using NUnit.Framework;
using Shared.Models.Infrastructure;

namespace ApiGateway.Client.Tests
{
    [TestFixture]
    public class CryptoTests
    {
        [Test]
        public async Task EncryptAndDecryptString()
        {
            var text = "some_text";
            var passpharse = "some123";


            var encrypted = await Crypto.EncryptAsync(text, passpharse);
            var decryptedText = await Crypto.DecryptAsync(encrypted, passpharse);

            Assert.AreEqual(text, decryptedText);
            Assert.CatchAsync<CryptographicException>(async () => await Crypto.DecryptAsync(encrypted, "wrong_passpharse"));
        }
    }
}