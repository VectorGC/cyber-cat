namespace ApiGateway.Client.Tests.WebClient
{
    public class TestEnvironment
    {
        public const string Host = "http://localhost:5000";
        public static IClient Client => new Client(Host, new WebClientAdapter());

        public static IRestClient RestClient => new WebClientAdapter();
    }
}