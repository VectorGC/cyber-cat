using System.Threading.Tasks;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    [TestFixture]
    public class AuthenticateTests
    {
        [Test]
        public async Task AuthenticateDefaultUser_WhenPassValidCredentials()
        {
            var client = TestClient.TestClient.Anonymous();
            var token = await client.Authorization.GetAuthenticationToken(TestClient.TestClient.TestEmail, TestClient.TestClient.TestUserPassword);

            Assert.IsNotEmpty(token);
        }
    }
}