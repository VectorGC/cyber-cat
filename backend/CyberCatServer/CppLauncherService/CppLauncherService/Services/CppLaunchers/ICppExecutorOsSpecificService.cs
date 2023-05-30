using CppLauncherService.InternalModels;

namespace CppLauncherService.Services.CppLaunchers
{
    internal interface ICppExecutorOsSpecificService
    {
        Task<CompileCppResult> CompileCode(string sourceCode);
        Task<Output> LaunchCode(string objectFileName, string input);
    }
}