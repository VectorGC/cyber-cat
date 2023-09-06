using System.Threading.Tasks;
using ApiGateway.Client.Models;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class UserClientTestFixture
    {
        private IUser _user;
        private readonly AnonymousClientTestFixture _anonymousClientTestFixture;

        public UserClientTestFixture(ServerEnvironment serverEnvironment)
        {
            _anonymousClientTestFixture = new AnonymousClientTestFixture(serverEnvironment);
        }

        public async Task<IUser> GetUserClient()
        {
            var anonymous = _anonymousClientTestFixture.GetAnonymousClient();
            if (_user == null)
            {
                await anonymous.SignUp("test@test.com", "test_password", "Test_Name");
            }

            _user = await anonymous.SignIn("test@test.com", "test_password");
            return _user;
        }

        [TearDown]
        public async Task TearDown()
        {
            var client = await GetUserClient();
            await client.Remove("test_password");
            _user = null;
        }
    }
}