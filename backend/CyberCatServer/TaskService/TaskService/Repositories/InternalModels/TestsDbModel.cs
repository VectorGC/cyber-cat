using Shared.Models.Dto;

namespace TaskService.Repositories.InternalModels;

internal class TestsDbModel : List<TestDbModel>
{
    public TestsDto ToDto()
    {
        return new TestsDto()
        {
            Tests = this.Select(test => test.ToDto()).ToList()
        };
    }
}