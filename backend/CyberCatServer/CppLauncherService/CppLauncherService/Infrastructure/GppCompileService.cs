using CppLauncherService.Application;
using CppLauncherService.Domain;

namespace CppLauncherService.Infrastructure;

public class GppCompileService : ICppCompileService
{
    private readonly IConsoleExecutor _consoleExecutor;

    public GppCompileService(IConsoleExecutor consoleExecutor)
    {
        _consoleExecutor = consoleExecutor;
    }

    public async Task<CompileCppFileResult> CompileCppFile(CppFile cppFile)
    {
        var cppFileName = cppFile.FileName;
        await using (var writer = File.CreateText(cppFileName))
        {
            await writer.WriteAsync(cppFile.Content);
        }

        var objectFile = new ObjectFile(cppFile);
        var compileCommand = new ConsoleCommand("g++", $"-g {cppFileName} -Wall -o {objectFile.FileName} -static-libgcc -static-libstdc++");

        var output = await _consoleExecutor.Run(compileCommand);

        return new CompileCppFileResult(!output.HasError, output.StandardError, output.HasError ? null : objectFile);
    }

    public async Task<Output> LaunchCode(ObjectFile objectFile, string[] inputs)
    {
        var input = inputs != null ? string.Join(" ", inputs) : string.Empty;
        var command = new ConsoleCommand($"./{objectFile.FileName}", string.Empty, input);
        var output = await _consoleExecutor.Run(command);

        return output;
    }

    public async Task Delete(CppFile cppFile)
    {
        var command = new ConsoleCommand("rm", cppFile.FileName);
        await _consoleExecutor.Run(command);
    }

    public async Task Delete(ObjectFile objectFile)
    {
        var command = new ConsoleCommand("rm", objectFile.FileName);
        await _consoleExecutor.Run(command);
    }
}