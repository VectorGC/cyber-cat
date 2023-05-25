using Shared.Models;

namespace TaskService.Repositories.InternalModels;

internal class TestModel : ITest
{
    public string Input { get; set; }
    public string ExpectedOutput { get; set; }
}