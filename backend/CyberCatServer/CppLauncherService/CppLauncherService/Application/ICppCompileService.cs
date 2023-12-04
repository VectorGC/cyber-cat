using CppLauncherService.Domain;

namespace CppLauncherService.Application;

public interface ICppCompileService
{
    Task<CompileCppFileResult> CompileCppFile(CppFile cppFile);
    Task<Output> LaunchCode(ObjectFile objectFile, string[] inputs);
    Task Delete(CppFile cppFile);
    Task Delete(ObjectFile objectFile);
}

public record CompileCppFileResult(bool Success, string Error, ObjectFile ObjectFile);