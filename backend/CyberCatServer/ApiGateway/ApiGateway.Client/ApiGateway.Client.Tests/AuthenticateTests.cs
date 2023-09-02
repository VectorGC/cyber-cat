using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.Tests.Abstracts;
using ApiGateway.Client.Tests.Extensions;
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
            const string email = "test@test.com";
            const string password = "test_password";
            const string userName = "Test_User";
            const string wrongPassword = "wrong_test_password";

            var anonymous = GetAnonymousClient();

            Assert.ThrowsAsync<WebException>(async () => await anonymous.SignIn(email, password)).AreEqual(HttpStatusCode.NotFound);

            await anonymous.SignUp(email, password, userName);

            // User is already registered.
            Assert.ThrowsAsync<WebException>(async () => await anonymous.SignUp(email, password, userName)).AreEqual(HttpStatusCode.Conflict);
            // Wrong password.
            Assert.ThrowsAsync<WebException>(async () => await anonymous.SignIn(email, wrongPassword)).AreEqual(HttpStatusCode.Forbidden);

            var client = await anonymous.SignIn(email, password);
            Assert.IsNotNull(client);

            // Wrong password when removing.
            Assert.ThrowsAsync<WebException>(async () => await client.Remove(wrongPassword)).AreEqual(HttpStatusCode.Forbidden);

            await client.Remove(password);

            Assert.ThrowsAsync<WebException>(async () => await anonymous.SignIn(email, password)).AreEqual(HttpStatusCode.NotFound);
        }
    }
}