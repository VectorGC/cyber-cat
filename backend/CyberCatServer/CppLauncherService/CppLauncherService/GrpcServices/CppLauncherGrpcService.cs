using CppLauncherService.InternalModels;
using CppLauncherService.Services;
using Shared.Server.Data;
using Shared.Server.Services;

namespace CppLauncherService.GrpcServices;

internal class CppLauncherGrpcService : ICodeLauncherGrpcService
{
    private readonly IProcessExecutorProxy _processExecutorProxy;
    private readonly ICppFileCreator _cppFileCreator;
    private readonly ICppErrorFormatService _errorFormatService;
    private readonly ILogger<CppLauncherGrpcService> _logger;

    public CppLauncherGrpcService(IProcessExecutorProxy processExecutorProxy, ICppFileCreator cppFileCreator,
        ICppErrorFormatService errorFormatService, ILogger<CppLauncherGrpcService> logger)
    {
        _processExecutorProxy = processExecutorProxy;
        _cppFileCreator = cppFileCreator;
        _errorFormatService = errorFormatService;
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
        var compileResult = await CompileCode(sourceCode);
        if (compileResult.Output.HasError)
        {
            await _processExecutorProxy.Run(RunCommand.DeleteFile(compileResult.ObjectFileName));
            await _processExecutorProxy.Run(RunCommand.DeleteFile($"{Path.GetFileNameWithoutExtension(compileResult.ObjectFileName)}.cpp"));
            return new OutputDto
            {
                StandardError = compileResult.Output.StandardError
            };
        }

        var launchOutput = await LaunchCode(compileResult.ObjectFileName, inputs);
        launchOutput = _errorFormatService.Format(launchOutput);

        await _processExecutorProxy.Run(RunCommand.DeleteFile(compileResult.ObjectFileName));
        await _processExecutorProxy.Run(RunCommand.DeleteFile($"{Path.GetFileNameWithoutExtension(compileResult.ObjectFileName)}.cpp"));

        return new OutputDto
        {
            StandardOutput = launchOutput.StandardOutput,
            StandardError = launchOutput.StandardError
        };
    }

    private async Task<CompileCppResult> CompileCode(string sourceCode)
    {
        var cppFileName = await _cppFileCreator.CreateCppWithText(sourceCode);
        var objectFileName = _cppFileCreator.GetObjectFileName(cppFileName);

        var output = await _processExecutorProxy.Run(RunCommand.CreateCompile(cppFileName, objectFileName));

        return new CompileCppResult
        {
            Output = output,
            ObjectFileName = objectFileName
        };
    }

    private async Task<Output> LaunchCode(string objectFileName, string[] inputs)
    {
        return await _processExecutorProxy.Run(RunCommand.CreateLaunch(objectFileName, inputs));
    }
}