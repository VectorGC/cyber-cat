using Shared.Models.Dto;
using Shared.Models.Models;

namespace TaskService.Repositories;

public interface ITestRepository
{
    Task Add(TaskId taskId, TestDto test);
    Task Add(TaskId taskId, TestsDto tests);
    Task<TestsDto> GetTests(TaskId taskId);
}