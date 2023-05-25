using Shared.Models;

namespace TaskService.Repositories.InternalModels;

internal class TestsModel : List<TestModel>, ITests
{
    public IEnumerator<ITest> GetEnumerator()
    {
        return base.GetEnumerator();
    }

    public ITest this[int index] => base[index];

    public TestsModel(IEnumerable<TestModel> tests)
        : base(tests)
    {
    }

    public IReadOnlyList<ITest> Tests
    {
        get => this;
        init => this.AddRange(value.Select(t => t.To<TestModel>()));
    }
}