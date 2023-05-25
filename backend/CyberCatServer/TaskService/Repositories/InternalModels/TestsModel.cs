using System.Collections;
using Shared.Models;

namespace TaskService.Repositories.InternalModels;

internal class TestsModel : ITests
{
    private readonly List<TestModel> _tests;

    public TestsModel(List<TestModel> tests)
    {
        _tests = tests;
    }

    public IEnumerator<ITest> GetEnumerator()
    {
        return _tests.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _tests.GetEnumerator();
    }

    public int Count => _tests.Count;

    public ITest this[int index] => _tests[index];
}