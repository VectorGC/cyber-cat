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

    public async Task<LaunchCodeResponse> Launch(SourceCodeArgs args)
    {
        var sourceCode = args.SourceCode;

        var compileResult = await _compileService.CompileCode(sourceCode);
        if (compileResult.Output.HasError)
        {
            return new LaunchCodeResponse
            {
                StandardError = compileResult.Output.StandardError
            };
        }

        var launchOutput = await _compileService.LaunchCode(compileResult.ObjectFileName);
        return new LaunchCodeResponse
        {
            StandardOutput = launchOutput.StandardOutput,
            StandardError = launchOutput.StandardError
        };
    }
}