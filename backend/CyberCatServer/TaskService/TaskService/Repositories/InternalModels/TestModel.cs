using Shared.Models.Models;

namespace TaskService.Repositories.InternalModels;

internal class TestModel : ITest
{
    public string Input { get; set; }
    public string ExpectedOutput { get; set; }

    public TestModel(ITest test)
    {
        Input = test.Input;
        ExpectedOutput = test.ExpectedOutput;
    }

    public TestModel()
    {
    }
}