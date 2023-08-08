using System.Threading.Tasks;

namespace ApiGateway.Client.Tests.TestClient
{
    public static class TestClient
    {
        public const string ServerUri
            //= "http://localhost:5000";
        //= "http://localhost";
        // = "http://server.cyber-cat.pro";
         = "https://server.cyber-cat.pro";

        public const string TestEmail = "cat";
        public const string TestUserPassword = "cat";

        public static IAnonymousClient Anonymous()
        {
            return ServerClient.Create(ServerUri);
        }

        public static async Task<IAuthorizedClient> Authorized()
        {
            var anonymous = Anonymous();
            var token = await anonymous.Authorization.GetAuthenticationToken(TestEmail, TestUserPassword);

            return ServerClient.Create(ServerUri, token);
        }
    }
}