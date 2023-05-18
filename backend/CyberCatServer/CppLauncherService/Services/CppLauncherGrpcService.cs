using CompilerServiceAPI.Services.CppLaunchers;
using Shared.Dto;
using Shared.Services;

namespace CompilerServiceAPI.Services;

internal class CppLauncherGrpcService : ICodeLauncherGrpcService
{
    private readonly ICppLauncherService _compileService;

    public CppLauncherGrpcService(ICppLauncherService compileService)
    {
        _compileService = compileService;
    }

    public async Task<LaunchCodeResponse> Launch(SolutionCodeArgs args)
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