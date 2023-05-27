namespace Shared.Models;

public interface ISolution
{
    string TaskId { get; init; }
    string SourceCode { get; init; }

    T To<T>() where T : ISolution, new()
    {
        if (this is T typed)
        {
            return typed;
        }

        return new T
        {
            TaskId = TaskId,
            SourceCode = SourceCode
        };
    }
}