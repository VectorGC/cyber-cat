using System;
using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.Clients;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    public abstract class AuthorizedClientTestFixture : AnonymousClientTestFixture
    {
        protected AuthorizedClientTestFixture(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        protected async Task<IAuthorizedClient> GetAuthorizedClient()
        {
            try
            {
                return await ServerClientFactory.CreateAuthorized(ServerEnvironment, "test@test.com", "test_password");
            }
            catch (WebException ex) when (ex.Response is HttpWebResponse httpWebResponse && httpWebResponse.StatusCode == HttpStatusCode.NotFound)
            {
                var anonymous = GetAnonymousClient();
                await anonymous.RegisterUser("test@test.com", "test_password", "Test_Name");

                return await ServerClientFactory.CreateAuthorized(ServerEnvironment, "test@test.com", "test_password");
            }
        }

        [TearDown]
        public async Task TearDown()
        {
            var client = await GetAuthorizedClient();
            await client.RemoveUser();
        }
    }
}