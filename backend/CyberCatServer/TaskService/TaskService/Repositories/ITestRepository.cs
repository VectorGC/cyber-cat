using Shared.Models.Ids;
using Shared.Server.Dto;

namespace TaskService.Repositories;

public interface ITestRepository
{
    Task Add(TaskId taskId, TestsDto tests);
    Task<TestsDto> GetTests(TaskId taskId);
}