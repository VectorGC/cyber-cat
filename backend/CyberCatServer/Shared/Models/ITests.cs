namespace Shared.Models;

public interface ITests : IReadOnlyCollection<ITest>
{
    T To<T>() where T : IAdd<ITest>, new()
    {
        if (this is T typed)
        {
            return typed;
        }

        var instance = new T();
        foreach (var test in this)
        {
            instance.Add(test);
        }

        return instance;
    }
}