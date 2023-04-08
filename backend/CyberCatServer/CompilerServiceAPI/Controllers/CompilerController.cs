using CompilerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CompileCode(string code)
        {
            //Запрос для компиляции кода

            //Создаем cpp файл с кодом из запроса
            using (StreamWriter writer = System.IO.File.CreateText("code.cpp"))
            {
                writer.Write(code);
            }
            
            //Компилируем cpp файл с помощью mingw
            var res = _commandService.RunDockerCommandWithOverrideOutput("x86_64-w64-mingw32-g++", "code.cpp -Wall -o code.exe -static-libgcc -static-libstdc++");

            //Запускаем полученный exe с помощью wine и получаем строку из стандартного потока вывода процесса
            //var res = _commandService.RunDockerCommandWithOverrideOutput("wine", "code.exe");
            return Ok(res);
        }
    }
}
