using CompilerServiceAPI.InternalModels;

namespace CompilerServiceAPI.Services.CppLaunchers
{
    internal interface ICppLauncherService
    {
        Task<CompileCppResult> CompileCode(string sourceCode);
        Task<Output> LaunchCode(string objectFileName);
    }
}