using System.Threading.Tasks;
using ApiGateway.Client.Models;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    public abstract class UserClientTestFixture : AnonymousClientTestFixture
    {
        private IUser _user;

        protected UserClientTestFixture(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        protected async Task<IUser> GetUserClient()
        {
            var anonymous = GetAnonymousClient();
            if (_user == null)
            {
                await anonymous.SignUp("test@test.com", "test_password", "Test_Name");
            }

            _user = await anonymous.SignIn("test@test.com", "test_password");
            return _user;
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await OnTearDown();
        }

        protected virtual async Task OnTearDown()
        {
            var client = await GetUserClient();
            await client.Remove("test_password");
            _user = null; 
        }
    }
}