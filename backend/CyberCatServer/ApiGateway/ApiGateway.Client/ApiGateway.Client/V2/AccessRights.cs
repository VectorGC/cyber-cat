using System;
using System.Collections.Generic;

namespace ApiGateway.Client.V2
{
    public class AccessRights : IRole
    {
        private readonly Dictionary<Type, IAccess> _accessRights = new Dictionary<Type, IAccess>();

        public AccessRights(ServerEnvironment serverEnvironment)
        {
            Register(new Credentials());
            Register(new WebClient(serverEnvironment, Access<Credentials>()));
            Register(new VK(Access<WebClient>(), Access<Credentials>()));
            Register(new Dev(Access<WebClient>()));
        }

        public T Access<T>() where T : class, IAccess
        {
            if (!_accessRights.TryGetValue(typeof(T), out var access))
            {
                return default;
            }

            if (!access.IsAvailable)
            {
                return default;
            }

            return access as T;
        }

        private void Register(IAccess access)
        {
            _accessRights.Add(access.GetType(), access);
        }

        public void Dispose()
        {
            foreach (var access in _accessRights.Values)
            {
                access.Dispose();
            }

            _accessRights.Clear();
        }
    }
}