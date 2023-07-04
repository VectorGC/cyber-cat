using System.Threading.Tasks;
using Shared.Models.Models;

namespace ApiGateway.Client.Tests
{
    public class PlayerClient : IClient
    {
        public const string TestEmail = "cat";
        public const string TestUserPassword = "cat";

        private readonly IClient _client;

        public static async Task<IClient> Create()
        {
            var client = new PlayerClient();
            var token = await client.Authenticate(TestEmail, TestUserPassword);

            client.AddAuthorizationToken(token);
            return client;
        }

        private PlayerClient()
        {
            //var uri = "http://localhost:5000";
            //var uri = "http://localhost";
            //var uri = "http://server.cyber-cat.pro";
            var uri = "https://server.cyber-cat.pro";
            _client = new Client(uri, new WebClientAdapter());
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void AddAuthorizationToken(string token)
        {
            _client.AddAuthorizationToken(token);
        }

        public void RemoveAuthorizationToken()
        {
            _client.RemoveAuthorizationToken();
        }

        public Task<string> Authenticate(string email, string password)
        {
            return _client.Authenticate(email, password);
        }

        public Task<string> AuthorizePlayer(string token)
        {
            return _client.AuthorizePlayer(token);
        }

        public Task<ITask> GetTask(string taskId)
        {
            return _client.GetTask(taskId);
        }

        public Task<string> GetSavedCode(string taskId)
        {
            return _client.GetSavedCode(taskId);
        }

        public Task SaveCode(string taskId, string sourceCode)
        {
            return _client.SaveCode(taskId, sourceCode);
        }

        public Task RemoveSavedCode(string taskId)
        {
            return _client.RemoveSavedCode(taskId);
        }

        public async Task<string> GetStringTestAsync()
        {
            return await _client.GetStringTestAsync();
        }

        public Task<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            return _client.VerifySolution(taskId, sourceCode);
        }
    }
}