using System;

namespace ApiGateway.Client.V2
{
    public static class ApiGateway
    {
        public class Client : IDisposable
        {
            public UserV2 User { get; }

            public Client(ServerEnvironment serverEnvironment)
            {
                User = new UserV2(serverEnvironment);
            }

            public void Dispose()
            {
                User.Dispose();
            }
        }
    }
}