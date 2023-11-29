using ApiGateway.Client.V3.Application;
using Shared.Models.Domain.Users;

namespace ApiGateway.Client.V3.Domain
{
    public class PlayerModel
    {
        public ITaskRepository Tasks { get; }
        public UserModel User { get; }

        public PlayerModel(UserModel user)
        {
            User = user;
        }
    }
}