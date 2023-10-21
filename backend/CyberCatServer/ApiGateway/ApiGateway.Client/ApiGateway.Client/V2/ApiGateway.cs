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
                var role = new AccessRights(serverEnvironment);
                User = new UserV2(role);
            }

            public void Dispose()
            {
                User.Dispose();
            }
        }
    }

    public class UserV2 : IDisposable
    {
        public string Email => Access<Credentials>()?.Email;
        public string Name => Access<Credentials>()?.Name;

        private readonly IRole _role;

        public UserV2(IRole role)
        {
            _role = role;
        }

        public T Access<T>() where T : class, IAccess
        {
            return _role.Access<T>();
        }

        public void Dispose()
        {
            _role.Dispose();
        }
    }
}