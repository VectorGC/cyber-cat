using System.Threading.Tasks;

namespace ApiGateway.Client.Factory
{
    public readonly struct ClientAuthorizedCredentialsBuilder
    {
        private readonly string _email;
        private readonly string _password;

        public ClientAuthorizedCredentialsBuilder(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public async Task<IAuthorizedClient> Create(ServerEnvironment serverEnvironment)
        {
            var anonymous = ServerClientFactory.Create(serverEnvironment);
            var token = await anonymous.Authorization.GetAuthenticationToken(_email, _password);

            return ServerClientFactory.UseToken(token).Create(serverEnvironment);
        }
    }
}