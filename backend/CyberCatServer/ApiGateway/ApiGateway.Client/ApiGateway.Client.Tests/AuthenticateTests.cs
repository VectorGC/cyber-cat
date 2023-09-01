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
            var email = "test@test.com";
            var password = "test_password";
            var userName = "Test_User";
            var wrongPassword = "wrong_test_password";

            var anonymous = GetAnonymousClient();

            var ex = Assert.ThrowsAsync<WebException>(async () => await anonymous.Authorize(email, password));
            var response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            await anonymous.RegisterUser(email, password, userName);

            // User is already registered.
            ex = Assert.ThrowsAsync<WebException>(async () => await anonymous.RegisterUser(email, password, userName));
            response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);

            ex = Assert.ThrowsAsync<WebException>(async () => await anonymous.Authorize(email, wrongPassword));
            response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);

            var client = await anonymous.Authorize(email, password);
            Assert.IsNotNull(client);

            await client.RemoveUser();

            ex = Assert.ThrowsAsync<WebException>(async () => await anonymous.Authorize(email, password));
            response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            ex = Assert.ThrowsAsync<WebException>(async () => await client.AuthorizePlayer());
            response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}