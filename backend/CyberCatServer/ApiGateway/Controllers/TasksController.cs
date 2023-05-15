using System.Net;
using System.Text.Json.Nodes;
using ApiGateway.Dto;
using ApiGateway.Repositories;
using ApiGateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ITaskRepository _taskRepository;

    public TasksController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(JsonObject), (int) HttpStatusCode.Forbidden)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskDto>> GetTask(string id)
    {
        var task = await _taskRepository.GetTask(id);
        return TaskDto.FromTask(task);
    }
}