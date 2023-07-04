using System.Threading.Tasks;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Tests
{
    [TestFixture]
    public class GetTestAsync
    {
        [Test]
        public async Task Test()
        {
            var client = new Client("http://localhost:5000", new WebClientAdapter());
            var response = await client.GetStringTestAsync();

            Assert.AreEqual("Hello", response);
        }
    }
}