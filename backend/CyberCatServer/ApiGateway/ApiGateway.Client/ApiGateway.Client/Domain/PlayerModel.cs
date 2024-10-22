using Shared.Models.Domain.Users;

namespace ApiGateway.Client.Domain
{
    public class PlayerModel
    {
        public string UserName => User.FirstName;
        public TaskCollection Tasks { get; }
        public UserModel User { get; }

        public PlayerModel(UserModel user, TaskCollection taskCollection)
        {
            User = user;
            Tasks = taskCollection;
        }
    }
}