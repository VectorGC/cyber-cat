using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Application.API;
using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Domain;

namespace ApiGateway.Client.Tests.Extensions
{
    public class TestPlayerClient : IApiGatewayClient
    {
        public PlayerAPI Player => Client.Player;
        public ApiGatewayClient Client { get; }
        public string Password { get; }

        public TestPlayerClient(ApiGatewayClient client, string password)
        {
            Password = password;
            Client = client;
        }

        public void Dispose()
        {
            Client.Player.Remove(Password);
            Client.Dispose();
        }

        public Task<Result> RegisterPlayer(string email, string password, string userName)
        {
            return Client.RegisterPlayer(email, password, userName);
        }

        public Task<Result<PlayerModel>> LoginPlayer(string email, string password)
        {
            return Client.LoginPlayer(email, password);
        }
    }
}