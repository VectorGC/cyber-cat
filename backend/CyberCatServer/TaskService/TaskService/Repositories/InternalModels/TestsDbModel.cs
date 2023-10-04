using Shared.Models.Ids;
using Shared.Models.Models;

namespace TaskService.Repositories.InternalModels;

internal class TestsDbModel : List<TestDbModel>
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