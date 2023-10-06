using Shared.Models.Ids;
using Shared.Models.Models.TestCases;

namespace TaskService.Repositories.InternalModels;

internal class TestDbModel
{
    public string[] Inputs { get; set; }
    public string Expected { get; set; }

    public TestDbModel(TestCase test)
    {
        Inputs = test.Inputs;
        Expected = test.Expected;
    }

    public TestDbModel()
    {
    }

    public TestCase ToDescription(TaskId taskId, int index)
    {
        return new TestCase()
        {
            Id = new TestCaseId(taskId, index),
            Inputs = Inputs,
            Expected = Expected
        };
    }
}