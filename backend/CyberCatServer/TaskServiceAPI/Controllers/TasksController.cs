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
        private readonly ITaskCollection _taskCollection;

        public TasksController(ITaskService taskService, ITaskCollection taskCollection)
        {
            _taskService = taskService;
            _taskCollection = taskCollection;
        }

        [HttpPut]
        public async Task<IActionResult> AddTask(ProgTask task) 
        {
            await _taskService.Add(task);
            return Ok(task);
        }

        [HttpGet]
        public async Task <IActionResult> GetTask(int id)
        {
            var task = await _taskCollection.GetTask(id);
            return Ok(task);
        }
    }
}
