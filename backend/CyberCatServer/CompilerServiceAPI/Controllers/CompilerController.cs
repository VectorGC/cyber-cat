using CompilerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CompilerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompilerController : ControllerBase
    {
        public struct TaskCode
        {
            public string Code { get; set; }
        }
        private readonly ICommandService _commandService;
        public CompilerController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost]
        public IActionResult CheckVersion(TaskCode code)
        {
            using (StreamWriter writer = System.IO.File.CreateText("code.cpp"))
            {
                writer.Write(code.Code);
            }
            _commandService.RunDockerCommand("x86_64-w64-mingw32-g++", "code.cpp -o code.exe -static-libgcc -static-libstdc++");
            _commandService.RunDockerCommand("wine64", "code.exe");
            //System.IO.File.Delete("code.txt");
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(id);
        }

        [HttpPut]
        public void Put(int id)
        {
            _commandService.RunDockerCommand("apt", "update");
            _commandService.RunDockerCommand("apt", "-y install mingw-w64");
            _commandService.RunDockerCommand("apt", "-y install wine64");
            _commandService.RunDockerCommand("x86_64-w64-mingw32-g++", "--version");
            _commandService.RunDockerCommand("wine64", "--version");
        }
    }
}
