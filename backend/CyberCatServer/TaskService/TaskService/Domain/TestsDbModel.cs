using Shared.Models.Domain.Tasks;
using Shared.Models.Models.TestCases;

namespace TaskService.Domain;

public class TestsDbModel : List<TestDbModel>
{
    public TestCases ToDescription(TaskId taskId)
    {
        var testCases = new TestCases();
        for (var i = 0; i < Count; i++)
        {
            var test = this[i].ToDescription(taskId, i);
            testCases.Add(test);
        }

        return testCases;
    }
}