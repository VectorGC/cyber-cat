using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Descriptions;
using Shared.Models.Ids;
using Shared.Models.ProtoHelpers;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TasksController : ControllerBase
{
    private readonly ITaskGrpcService _taskGrpcService;

    public TasksController(ITaskGrpcService taskGrpcService)
    {
        _taskGrpcService = taskGrpcService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(TaskIdsDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskIdsDto>> GetTasks()
    {
        var response = await _taskGrpcService.GetTasks();
        response.EnsureSuccess();

        var taskIds = response.Value.Select(taskId => taskId.Value).ToList();
        var responseDto = new TaskIdsDto
        {
            taskIds = taskIds
        };

        return responseDto;
    }

    [HttpGet("{taskId}")]
    [ProducesResponseType(typeof(TaskDescription), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskDescription>> GetTask(string taskId)
    {
        var response = await _taskGrpcService.GetTask(taskId);
        return response;
    }

    [HttpGet("{taskId}/tests")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<string>> GetTestCases(string taskId)
    {
        var response = await _taskGrpcService.GetTestCases(taskId);
        return response.Value.ToProtobufBytesString();
    }
}