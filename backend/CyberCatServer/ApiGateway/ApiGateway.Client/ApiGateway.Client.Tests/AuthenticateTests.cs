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
            await AssertAsync.ThrowsWebException(async () => await anonymous.SignIn(email, password), HttpStatusCode.NotFound);

            await anonymous.SignUp(email, password, userName);

            // User is already registered.
            await AssertAsync.ThrowsWebException(async () => await anonymous.SignUp(email, password, userName), HttpStatusCode.Conflict);

            // Wrong password.
            await AssertAsync.ThrowsWebException(async () => await anonymous.SignIn(email, wrongPassword), HttpStatusCode.Forbidden);

            var client = await anonymous.SignIn(email, password);
            Assert.IsNotNull(client);

            // Wrong password when removing.
            await AssertAsync.ThrowsWebException(async () => await client.Remove(wrongPassword), HttpStatusCode.Forbidden);

            await client.Remove(password);

            await AssertAsync.ThrowsWebException(async () => await anonymous.SignIn(email, password), HttpStatusCode.NotFound);
        }
    }
}