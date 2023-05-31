using CppLauncherService.InternalModels;

namespace CppLauncherService.Services
{
    internal interface IProcessExecutorProxy
    {
        Task<Output> Run(RunCommand command);
    }
}