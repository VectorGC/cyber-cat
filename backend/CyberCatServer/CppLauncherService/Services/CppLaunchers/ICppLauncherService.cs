using CompilerServiceAPI.InternalModels;

namespace CompilerServiceAPI.Services.CppLaunchers
{
    internal interface ICppLauncherService
    {
        Task<Output> CompileCode(string sourceCode);
        Task<Output> LaunchCode();
    }
}