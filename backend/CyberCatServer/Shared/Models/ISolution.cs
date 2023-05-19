namespace Shared.Models;

public interface ISolution
{
    string UserId { get; init; }
    string TaskId { get; init; }
    string SourceCode { get; init; }

    T To<T>() where T : class, ISolution, new()
    {
        return this as T ?? new T
        {
            UserId = UserId,
            TaskId = TaskId,
            SourceCode = SourceCode
        };
    }
}