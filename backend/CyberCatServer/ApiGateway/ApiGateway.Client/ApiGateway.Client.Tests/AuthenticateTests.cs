using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
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
            var anonymous = GetAnonymousClient();
            await anonymous.RegisterUser("test@test.com", "test_password", "Test_User");

            var client = await anonymous.Authorize("test@test.com", "test_password");
            Assert.IsNotNull(client);

            await client.RemoveUser();

            var ex = Assert.ThrowsAsync<WebException>(async () => await client.AuthorizePlayer());
            var response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}