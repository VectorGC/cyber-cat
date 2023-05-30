using CppLauncherService.Services;
using CppLauncherService.Services.CppLaunchers;
using Shared.Dto;
using Shared.Dto.Args;
using Shared.Services;

namespace CppLauncherService.GrpcServices;

internal class CppLauncherGrpcService : ICodeLauncherGrpcService
{
    private readonly ICppExecutorOsSpecificService _compileService;
    private readonly ICppErrorFormatService _errorFormatService;

    public CppLauncherGrpcService(ICppExecutorOsSpecificService compileService, ICppErrorFormatService errorFormatService)
    {
        _compileService = compileService;
        _errorFormatService = errorFormatService;
    }

    public async Task<OutputDto> Launch(LaunchCodeArgs args)
    {
        return await LaunchCode(args.SourceCode, args.Input);
    }

    private async Task<OutputDto> LaunchCode(string sourceCode, string input = null)
    {
        var compileResult = await _compileService.CompileCode(sourceCode);
        if (compileResult.Output.HasError)
        {
            return new OutputDto
            {
                StandardError = compileResult.Output.StandardError
            };
        }

        var launchOutput = await _compileService.LaunchCode(compileResult.ObjectFileName, input);
        launchOutput = _errorFormatService.Format(launchOutput);

        return new OutputDto
        {
            StandardOutput = launchOutput.StandardOutput,
            StandardError = launchOutput.StandardError
        };
    }
}