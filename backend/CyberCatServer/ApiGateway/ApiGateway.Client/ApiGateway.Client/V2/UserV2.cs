namespace ApiGateway.Client.V2
{
    public class UserV2
    {
        public string Email => _accessRights.Access<VK>().Email;
        public string Name => _accessRights.Access<VK>().FirstName;
        public bool IsAnonymous => !_accessRights.Access<VK>().IsSignedIn;

        private readonly AccessRights _accessRights;

        public UserV2(ServerEnvironment serverEnvironment)
        {
            _accessRights = new AccessRights(serverEnvironment)
            {
                [typeof(VK)] = env => new VK(env),
                [typeof(Dev)] = env => new Dev(env),
            };
        }

        public TAccess Access<TAccess>() where TAccess : class, IAccess
        {
            return _accessRights.Access<TAccess>();
        }

        public void Dispose()
        {
            _accessRights.Dispose();
        }
    }
}