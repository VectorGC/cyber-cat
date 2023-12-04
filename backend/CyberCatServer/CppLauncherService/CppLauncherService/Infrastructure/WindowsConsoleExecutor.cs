using CppLauncherService.Application;
using CppLauncherService.Domain;
using Microsoft.Extensions.Options;

namespace CppLauncherService.Infrastructure
{
    public class WindowsConsoleExecutor : IConsoleExecutor
    {
        private readonly LinuxConsoleExecutor _linuxConsoleExecutor;

        public WindowsConsoleExecutor(IOptions<CppLauncherAppSettings> appSettings, ILogger<WindowsConsoleExecutor> logger)
        {
            _linuxConsoleExecutor = new LinuxConsoleExecutor(appSettings, logger);
        }

        public async Task<Output> Run(ConsoleCommand command)
        {
            var wslCommand = new ConsoleCommand("wsl", $"{command.Command} {command.Arguments}", command.Input);
            return await _linuxConsoleExecutor.Run(wslCommand);
        }
    }
}