namespace Shared.Models;

public interface ITests
{
    IReadOnlyList<ITest> Tests { get; init; }

    T To<T, TTest>() where T : class, ITests, new() where TTest : class, ITest, new()
    {
        var tests = Tests.Select(test => test.To<TTest>()).ToList();
        return this as T ?? new T
        {
            Tests = tests
        };
    }
}