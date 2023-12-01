using System;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.V2.Extensions
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public abstract class ApiGatewayClientTestFixture
    {
        protected readonly ServerEnvironment ServerEnvironment;

        public ApiGatewayClientTestFixture(ServerEnvironment serverEnvironment)
        {
            ServerEnvironment = serverEnvironment;
        }

        public async Task<TestPlayerClient> GetTestPlayerClient(string email = "test@test.com", string password = "test_password", string userName = "Test_Name")
        {
            var client = new ApiGatewayClient(ServerEnvironment);

            var result = await client.RegisterPlayer(email, password, userName);
            if (!result.IsSuccess)
                throw new InvalidOperationException(result.Error);

            var loginResult = await client.LoginPlayer(email, password);
            if (!loginResult.IsSuccess)
                throw new InvalidOperationException(loginResult.Error);

            return new TestPlayerClient(client, password);
        }
    }
}