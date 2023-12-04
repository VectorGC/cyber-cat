using CppLauncherService.Domain;

namespace CppLauncherService.Application;

public interface IConsoleExecutor
{
    Task<Output> Run(ConsoleCommand command);
}