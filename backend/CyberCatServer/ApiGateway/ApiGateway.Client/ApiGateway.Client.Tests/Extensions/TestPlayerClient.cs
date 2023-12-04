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
        public string Password { get; set; }
        public string VkId { get; set; }

        public TestPlayerClient(ApiGatewayClient client)
        {
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
            Password = password;
            return Client.LoginPlayer(email, password);
        }

        public Task<Result<PlayerModel>> LoginPlayerWithVk(string email, string userName, string vkId)
        {
            VkId = vkId;
            return Client.LoginPlayerWithVk(email, userName, vkId);
        }
    }
}