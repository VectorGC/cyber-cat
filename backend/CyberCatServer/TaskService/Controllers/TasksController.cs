using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskService.Repositories;
using TaskService.Services;
using TaskService.Models;

namespace TaskService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _taskService;
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITasksService taskService, ITaskRepository taskRepository)
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
