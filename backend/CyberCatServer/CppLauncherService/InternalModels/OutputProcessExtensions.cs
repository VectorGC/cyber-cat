using System.Diagnostics;

namespace CompilerServiceAPI.InternalModels;

internal static class OutputProcessExtensions
{
    internal static async Task<Output> ReadError(this Process process)
    {
        return ReadError(process, await process.StandardError.ReadToEndAsync());
    }

    internal static Output ReadError(this Process process, string message)
    {
        return Output.Error(process.ExitCode, message);
    }

    internal static async Task<Output> ReadOk(this Process process)
    {
        return Output.Ok(await process.StandardOutput.ReadToEndAsync());
    }
}