namespace CompilerServiceAPI.Services
{

    public class WinCompileService : ICompileService
    {
        private readonly ICommandService _commandService;
        public WinCompileService(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public string CompileCode(string code)
        {
            code = "#include <stdio.h>\nint main() { printf(\"Hello from my library!\"); }";
            //Создаем cpp файл с кодом из запроса
            using (StreamWriter writer = System.IO.File.CreateText("code.cpp"))
            {
                writer.Write(code);
            }
            var res = string.Empty;
            //Компилируем cpp файл с помощью g++
            res = _commandService.RunDockerCommand("wsl", "g++ code.cpp -Wall -Werror -o code -static-libgcc -static-libstdc++");
            return res;
        }

        public string LaunchCode()
        {
            var res = string.Empty;
            res = _commandService.RunDockerCommand("wsl", "./code");
            return res;
        }
    }
}
