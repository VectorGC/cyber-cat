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

        var compileOutput = await _compileService.CompileCode(sourceCode);
        if (compileOutput.HasError)
        {
            return new LaunchCodeResponse
            {
                StandardError = compileOutput.StandardError
            };
        }

        var launchOutput = await _compileService.LaunchCode();
        return new LaunchCodeResponse
        {
            StandardOutput = launchOutput.StandardOutput,
            StandardError = launchOutput.StandardError
        };
    }
}