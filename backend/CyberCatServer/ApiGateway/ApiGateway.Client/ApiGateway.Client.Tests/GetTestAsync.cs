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
            var client = TestClient.Anonymous();
            var response = await client.GetStringTestAsync("http://localhost:5000/auth/simple");

            Assert.AreEqual("Hello", response);
        }
    }
}