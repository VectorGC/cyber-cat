using NUnit.Framework;
using Shared.Models;

namespace ApiGateway.Client.Tests.Infrastructure
{
    [TestFixture]
    public class GZipTests
    {
        [Test]
        public void Zip_Unzip()
        {
            var source = "askdjquiwheuiqwhdnqwuieyhqwuiheqoiwdnqwuheuiqwgheiuqwndiqwmopeqweqwdqwweqe";
            var result = GZIP.ZipToString(source);
            Assert.IsNotEmpty(result);

            var backResult = GZIP.UnzipFromString(result);
            Assert.AreEqual(source, backResult);
        }
    }
}