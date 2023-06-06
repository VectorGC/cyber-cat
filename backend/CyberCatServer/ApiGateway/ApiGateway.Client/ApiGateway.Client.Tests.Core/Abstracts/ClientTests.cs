using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Shared.Dto;
using Shared.Dto.Args;

namespace ApiGateway.Client.Tests.Core.Abstracts
{
    [TestFixture]
    public abstract class ClientTests
    {
        protected abstract string Host { get; }
        protected abstract IClient Client { get; }
        protected abstract IRestClient RestClient { get; }

        protected const string TestEmail = "test_user_name@test.com";
        protected const string TestUserPassword = "TestUserPassword123456@";
        protected const string TestUserName = "TestUserName";

        [SetUp]
        public async Task SetUp()
        {
            await AddTestUser();

            var token = await GetTokenForTestUser();
            Client.AddAuthorizationToken(token);
        }

        [TearDown]
        public async Task TearDown()
        {
            await RemoveTestUser();

            Client.Dispose();
            RestClient.Dispose();
        }

        private async Task AddTestUser()
        {
            var args = new CreateUserArgs()
            {
                User = new UserDto()
                {
                    UserName = TestUserName,
                    Email = TestEmail
                },
                Password = TestUserPassword
            };

            await RestClient.PostAsJsonAsync(Host + "/auth/create", args);
        }

        private async Task RemoveTestUser()
        {
            var token = await GetTokenForTestUser();
            RestClient.AddAuthorizationHeader(JwtBearerDefaults.AuthenticationScheme, token);

            await RestClient.PostStringAsync(Host + "/auth/remove", TestEmail);
            RestClient.RemoveAuthorizationHeader();
        }

        private async Task<string> GetTokenForTestUser()
        {
            return await GetTokenForUser(TestEmail, TestUserPassword);
        }

        private async Task<string> GetTokenForUser(string email, string password)
        {
            var form = new Dictionary<string, string>()
            {
                ["email"] = email,
                ["password"] = password
            };

            var token = await RestClient.PostAsync(Host + "/auth/login", form);
            return token;
        }
    }
}