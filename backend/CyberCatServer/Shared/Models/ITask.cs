namespace Shared.Models;

public interface ITask
{
    string Name { get; init; }
    string Description { get; init; }

    T To<T>() where T : ITask, new()
    {
        if (this is T typed)
        {
            return typed;
        }

        return new T
        {
            Name = Name,
            Description = Description
        };
    }
}