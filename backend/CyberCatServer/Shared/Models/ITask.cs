namespace Shared.Models;

public interface ITask
{
    string Name { get; init; }
    string Description { get; init; }

    T To<T>() where T : class, ITask, new()
    {
        return this as T ?? new T
        {
            Name = Name,
            Description = Description
        };
    }
}