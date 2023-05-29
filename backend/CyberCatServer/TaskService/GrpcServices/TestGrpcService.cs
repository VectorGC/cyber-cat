using Shared.Dto;
using Shared.Dto.ProtoHelpers;
using Shared.Services;
using TaskService.Repositories;

namespace TaskService.GrpcServices;

public class TestGrpcService : ITestGrpcService
{
    private readonly ITestRepository _testRepository;

    public TestGrpcService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async Task<TestsDto> GetTests(StringProto taskId)
    {
        var tests = await _testRepository.GetTests(taskId);
        return new TestsDto(tests);
    }
}