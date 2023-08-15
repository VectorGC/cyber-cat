using System.Threading.Tasks;
using ApiGateway.Client.Factory;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class AuthenticateTests : AnonymousClientTestFixture
    {
        public AuthenticateTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task AuthenticateDefaultUser_WhenPassValidCredentials()
        {
            var token = await Client.Authorization.GetAuthenticationToken("cat", "cat");

            Assert.IsNotEmpty(token);
        }
    }
}