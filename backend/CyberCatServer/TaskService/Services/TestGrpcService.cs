using Shared.Dto;
using Shared.Services;
using TaskService.Repositories;

namespace TaskService.Services;

public class TestGrpcService : ITestGrpcService
{
    private readonly ITestRepository _testRepository;

    public TestGrpcService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async Task<ListProto<TestDto>> GetTests(StringProto taskId)
    {
        var tests = await _testRepository.GetTests(taskId);
        return new ListProto<TestDto>()
        {
            Values = tests.Select(t => new TestDto
            {
                Input = t.Input,
                ExpectedOutput = t.ExpectedOutput
            }).ToList()
        };
    }
}