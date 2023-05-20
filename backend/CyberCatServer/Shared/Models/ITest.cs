namespace Shared.Models;

public interface ITest
{
    string Input { get; init; }
    string ExpectedOutput { get; init; }
    
    T To<T>() where T : class, ITest, new()
    {
        return this as T ?? new T
        {
            Input = Input,
            ExpectedOutput = ExpectedOutput
        };
    }
}