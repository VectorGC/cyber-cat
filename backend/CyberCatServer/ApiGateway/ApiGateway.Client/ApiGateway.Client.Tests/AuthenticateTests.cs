using System.Threading.Tasks;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Tests
{
    [TestFixture]
    public class AuthenticateTests
    {
        [Test]
        public async Task AuthenticateDefaultUser_WhenPassValidCredentials()
        {
            var client = TestClient.Anonymous();
            var token = await client.Authorization.GetAuthenticationToken(TestClient.TestEmail, TestClient.TestUserPassword);

            Assert.IsNotEmpty(token);
        }
    }
}