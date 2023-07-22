namespace Shared.Models.Models
{
    public interface IPlayer
    {
        long UserId { get; }
        int CompletedTasksCount { get; }
    }
}