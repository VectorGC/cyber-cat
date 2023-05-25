namespace Shared.Models;

public interface ITests
{
    IReadOnlyList<ITest> Tests { get; init; }
}