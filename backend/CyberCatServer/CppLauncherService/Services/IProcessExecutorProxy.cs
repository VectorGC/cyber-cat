using CompilerServiceAPI.InternalModels;

namespace CompilerServiceAPI.Services
{
    internal interface IProcessExecutorProxy
    {
        Task<Output> Run(string command, string arguments);
    }
}