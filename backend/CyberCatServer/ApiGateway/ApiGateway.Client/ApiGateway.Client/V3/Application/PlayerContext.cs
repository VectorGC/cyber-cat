using System;
using ApiGateway.Client.V3.Domain;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Application
{
    public class PlayerContext
    {
        public PlayerModel Player { get; private set; }
        public UserModel User { get; private set; }
        public AuthorizationToken Token { get; private set; }
        public bool IsLogined => Token != null && User != null && Player != null;

        public void SetContext(UserModel userModel, AuthorizationToken token)
        {
            if (!userModel.Roles.Has(Roles.Player))
                throw new InvalidOperationException("Role player mismatch");

            User = userModel;
            Player = new PlayerModel(userModel);
            Token = token;
        }

        public void Clear()
        {
            User = null;
            Player = null;
            Token = null;
        }
    }
}