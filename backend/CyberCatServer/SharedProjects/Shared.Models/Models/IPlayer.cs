namespace Shared.Models.Models
{
    public interface IPlayer
    {
        string UserId { get; }
        int CompletedTasksCount { get; }
        int BitcoinCount { get; }
    }
}