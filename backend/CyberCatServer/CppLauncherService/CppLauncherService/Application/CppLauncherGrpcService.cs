using CppLauncherService.Domain;
using Shared.Server.Application.Services;
using Shared.Server.Infrastructure;

namespace CppLauncherService.Application;

internal class CppLauncherGrpcService : ICodeLauncherService
{
    private readonly ICppCompileService _cppCompileService;
    private readonly ILogger<CppLauncherGrpcService> _logger;

    public CppLauncherGrpcService(ICppCompileService cppCompileService, ILogger<CppLauncherGrpcService> logger)
    {
        _cppCompileService = cppCompileService;
        _logger = logger;
    }

    public async Task<OutputDto> Launch(LaunchCodeArgs args)
    {
        _logger.LogInformation("Launch solution '{ArgsSolution}'", args.Solution);
        var (solution, inputs) = args;
        var output = await Launch(solution, inputs);

        _logger.LogInformation("Output '{Output}'", output.ToString());
        return output;
    }

    private async Task<OutputDto> Launch(string sourceCode, string[] inputs = null)
    {
        var fileName = Path.GetRandomFileName();
        var cppFile = new CppFile(fileName, sourceCode);

        var (compileSuccess, compileError, objectFile) = await _cppCompileService.CompileCppFile(cppFile);
        if (!compileSuccess)
        {
            await _cppCompileService.Delete(cppFile);
            return OutputDto.Error(compileError);
        }

        var launchOutput = await _cppCompileService.LaunchCode(objectFile, inputs);

        await _cppCompileService.Delete(cppFile);
        await _cppCompileService.Delete(objectFile);

        return new OutputDto
        {
            StandardOutput = launchOutput.StandardOutput,
            StandardError = launchOutput.StandardError
        };
    }
}