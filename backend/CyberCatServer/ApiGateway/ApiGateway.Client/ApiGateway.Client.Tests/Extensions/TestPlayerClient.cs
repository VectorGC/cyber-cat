using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application;

namespace ApiGateway.Client.Tests.Extensions
{
    public class TestPlayerClient : IDisposable
    {
        public ApiGatewayClient Client { get; }

        public static async Task<TestPlayerClient> Create(ServerEnvironment serverEnvironment, string email = "test@test.com", string password = "test_password", string userName = "Test_Name")
        {
            var client = new ApiGatewayClient(serverEnvironment);

            var result = await client.RegisterPlayer(email, password, userName);
            if (!result.IsSuccess)
                throw new InvalidOperationException(result.Error);

            var loginResult = await client.LoginPlayer(email, password);
            if (!loginResult.IsSuccess)
                throw new InvalidOperationException(loginResult.Error);

            return new TestPlayerClient(client);
        }

        private TestPlayerClient(ApiGatewayClient client)
        {
            Client = client;
        }

        public void Dispose()
        {
            Client.Player.Remove();
            Client.Dispose();
        }
    }
}