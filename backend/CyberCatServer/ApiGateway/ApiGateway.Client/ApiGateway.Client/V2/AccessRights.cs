using System;
using System.Collections.Generic;

namespace ApiGateway.Client.V2
{
    public class AccessRights : IRole
    {
        private readonly Dictionary<Type, IAccessV2> _accessRights = new Dictionary<Type, IAccessV2>();

        public AccessRights(ServerEnvironment serverEnvironment)
        {
            Register(new Credentials());
            Register(new WebClient(serverEnvironment, Access<Credentials>()));
            Register(new VK(Access<WebClient>(), Access<Credentials>()));
            Register(new Dev(Access<WebClient>()));
        }

        public T Access<T>() where T : class, IAccessV2
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

        private void Register(IAccessV2 access)
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