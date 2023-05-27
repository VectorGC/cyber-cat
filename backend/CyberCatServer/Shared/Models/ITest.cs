namespace Shared.Models;

public interface ITest
{
    string Input { get; init; }
    string ExpectedOutput { get; init; }

    T To<T>() where T : ITest, new()
    {
        if (this is T typed)
        {
            return typed;
        }

        return new T
        {
            Input = Input,
            ExpectedOutput = ExpectedOutput
        };
    }
}