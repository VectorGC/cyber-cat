using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Extensions
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public abstract class ApiGatewayClientTestFixture
    {
        protected readonly ServerEnvironment ServerEnvironment;
        protected readonly TestCodeSolution TestCodeSolution = new TestCodeSolution();

        public ApiGatewayClientTestFixture(ServerEnvironment serverEnvironment)
        {
            ServerEnvironment = serverEnvironment;
        }

        public async Task<TestPlayerClient> TestPlayerClientAsync(string email = "test@test.com", string password = "test_password", string userName = "Test_Name")
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

        public TestPlayerClient TestPlayerClient(string email = "test@test.com", string password = "test_password", string userName = "Test_Name")
        {
            return TestPlayerClientAsync(email, password, userName).Result;
        }

        public TestEnvironmentSetuper TestSetupClient()
        {
            return new TestEnvironmentSetuper();
        }
    }
}