using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServiceAPI.Repositories;
using TaskServiceAPI.Services;
using TaskServiceAPI.Models;

namespace TaskServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskService taskService, ITaskRepository taskRepository)
        {
            _taskService = taskService;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(ProgTaskDbModel task) 
        {
            await _taskService.Add(task);
            return Ok(task);
        }

        [HttpGet]
        public async Task <IActionResult> GetTask(int id)
        {
            var task = await _taskRepository.GetTask(id);
            return Ok(task);
        }
    }
}
