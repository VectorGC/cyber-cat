using Shared.Models;

namespace TaskService.Repositories.InternalModels;

internal class TestModel : ITest
{
    public string Input { get; init; }

    public string ExpectedOutput { get; init; }
}