using Shared.Models.Dto;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Models.Models;
using Shared.Server.Services;
using TaskService.Repositories;

namespace TaskService.GrpcServices;

public class TestGrpcService : ITestGrpcService
{
    private readonly ITestRepository _testRepository;

    public TestGrpcService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async Task<TestsDto> GetTests(TaskId taskId)
    {
        var tests = await _testRepository.GetTests(taskId);
        return tests;
    }
}