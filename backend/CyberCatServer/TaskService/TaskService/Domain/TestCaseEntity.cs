using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;

namespace TaskService.Domain;

public class TestCaseEntity
{
    public string[] Inputs { get; set; }
    public string Expected { get; set; }

    public TestCaseEntity(TestCaseDescription test)
    {
        Inputs = test.Inputs;
        Expected = test.Expected;
    }

    public TestCaseEntity()
    {
    }

    public TestCaseDescription ToDescription(TaskId taskId, int index)
    {
        return new TestCaseDescription()
        {
            Id = new TestCaseId(taskId, index),
            Inputs = Inputs,
            Expected = Expected
        };
    }
}