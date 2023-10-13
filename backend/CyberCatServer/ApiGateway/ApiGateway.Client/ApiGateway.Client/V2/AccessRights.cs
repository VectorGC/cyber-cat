using System;
using System.Collections.Generic;

namespace ApiGateway.Client.V2
{
    public class AccessRights : IDisposable
    {
        public Func<ServerEnvironment, IAccess> this[Type type]
        {
            get => _accessFactory[type];
            set => _accessFactory[type] = value;
        }

        private readonly Dictionary<Type, IAccess> _accessCache = new Dictionary<Type, IAccess>();
        private readonly Dictionary<Type, Func<ServerEnvironment, IAccess>> _accessFactory = new Dictionary<Type, Func<ServerEnvironment, IAccess>>();

        private readonly ServerEnvironment _serverEnvironment;

        public AccessRights(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        public TAccess Access<TAccess>() where TAccess : class, IAccess
        {
            if (_accessCache.TryGetValue(typeof(TAccess), out var cached))
            {
                return cached as TAccess;
            }

            if (_accessFactory.TryGetValue(typeof(TAccess), out var factoryFunc))
            {
                var access = factoryFunc.Invoke(_serverEnvironment);
                _accessCache[typeof(TAccess)] = access;

                return access as TAccess;
            }

            return null;
        }

        public void Dispose()
        {
            foreach (var access in _accessCache.Values)
            {
                access.Dispose();
            }

            _accessCache.Clear();
            _accessFactory.Clear();
        }
    }
}