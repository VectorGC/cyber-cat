using CppLauncherService.InternalModels;

namespace CppLauncherService.Services
{
    internal interface IProcessExecutorProxy
    {
        Task<Output> Run(string command, string arguments, string? input = null);
    }
}