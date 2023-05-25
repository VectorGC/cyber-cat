using Shared.Models;

namespace TaskService.Repositories.InternalModels;

internal class TestChallenge : ITest
{
    public string Input { get; set; }
    public string ExpectedOutput { get; set; }
}