namespace ApiGateway.Client.V2
{
    public static class ApiGateway
    {
        public class Client
        {
            public User User { get; }

            public Client(ServerEnvironment serverEnvironment)
            {
                User = new User(serverEnvironment);
            }
        }
    }
}