using System;
using ApiGateway.Client.V3;

namespace ApiGateway.Client.V2
{
    public static class ApiGateway
    {
        public class Client : IDisposable
        {
            public UserV2 User { get; }
            public UserV3 UserV3 { get; }

            public Client(ServerEnvironment serverEnvironment)
            {
                var role = new AccessRights(serverEnvironment);
                User = new UserV2(role);
                UserV3 = new UserV3(serverEnvironment);
            }

            public void Dispose()
            {
                User.Dispose();
            }
        }
    }
}