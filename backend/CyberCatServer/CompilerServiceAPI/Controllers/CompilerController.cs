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
            code = "#include <stdio.h>\nint main() { printf(\"Hello from my library!\"); }";

            //Создаем cpp файл с кодом из запроса
            using (StreamWriter writer = System.IO.File.CreateText("code.cpp"))
            {
                writer.Write(code);
            }
            
            //Компилируем cpp файл с помощью mingw
            var res = _commandService.RunDockerCommandWithOverrideOutput("wsl", "x86_64-w64-mingw32-g++ code.cpp -Wall -Werror -o code.exe -static-libgcc -static-libstdc++");

            if (!string.IsNullOrEmpty(res))
            {
                return Ok(res);
            }

            //Запускаем полученный exe с помощью wine и получаем строку из стандартного потока вывода процесса
            //var res2 = _commandService.RunDockerCommandWithOverrideOutput("wine", "code.exe");
            var res2 = _commandService.RunDockerCommandWithOverrideOutput("code.exe", "");
            return Ok(res2);
        }
    }
}
