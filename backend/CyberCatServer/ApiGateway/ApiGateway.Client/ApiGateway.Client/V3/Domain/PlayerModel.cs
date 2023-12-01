using Shared.Models.Domain.Users;

namespace ApiGateway.Client.V3.Domain
{
    public class PlayerModel
    {
        public TaskCollection Tasks { get; }
        public UserModel User { get; }

        public PlayerModel(UserModel user, TaskCollection taskCollection)
        {
            User = user;
            Tasks = taskCollection;
        }
    }
}