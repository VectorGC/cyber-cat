namespace Shared.Models.Models
{
    public interface IPlayer
    {
        long UserId { get; }
        string UserName { get;  }
        int CompletedTasksCount { get; }
    }
}