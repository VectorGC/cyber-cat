using CppLauncherService.Services.CppLaunchers;
using Shared.Dto;
using Shared.Services;

namespace CppLauncherService.Services;

internal class CppLauncherGrpcService : ICodeLauncherGrpcService
{
    private readonly ICppExecutorOsSpecificService _compileService;

    public CppLauncherGrpcService(ICppExecutorOsSpecificService compileService)
    {
        _compileService = compileService;
    }

    public async Task<OutputDto> Launch(StringProto sourceCode)
    {
        var compileResult = await _compileService.CompileCode(sourceCode);
        if (compileResult.Output.HasError)
        {
            return new OutputDto
            {
                StandardError = compileResult.Output.StandardError
            };
        }

        var launchOutput = await _compileService.LaunchCode(compileResult.ObjectFileName);
        return new OutputDto
        {
            StandardOutput = launchOutput.StandardOutput,
            StandardError = launchOutput.StandardError
        };
    }
}