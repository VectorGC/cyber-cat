using System.Diagnostics;
using System.Text;
using CppLauncherService.Application;
using CppLauncherService.Domain;
using Microsoft.Extensions.Options;

namespace CppLauncherService.Infrastructure
{
    public class LinuxConsoleExecutor : IConsoleExecutor
    {
        private readonly TimeSpan _timeOut;
        private readonly ILogger<WindowsConsoleExecutor> _logger;

        public LinuxConsoleExecutor(IOptions<CppLauncherAppSettings> appSettings, ILogger<WindowsConsoleExecutor> logger)
        {
            _logger = logger;
            _timeOut = appSettings.Value.ProcessTimeout;
        }

        public async Task<Output> Run(ConsoleCommand command)
        {
            _logger.LogInformation("Run '{Command}'", command.ToString());
            var startInfo = new ProcessStartInfo()
            {
                FileName = command.Command,
                Arguments = command.Arguments,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardErrorEncoding = Encoding.Default
            };

            var process = Process.Start(startInfo);

            if (!string.IsNullOrEmpty(command.Input))
            {
                await process.StandardInput.WriteAsync(command.Input);
                // After entering, you need to send the "\n" character (as if you pressed Enter) for the program to register the input.
                await process.StandardInput.WriteAsync(Environment.NewLine);
            }

            var output = await WaitForExit(process, _timeOut);
            return output;
        }

        private async Task<Output> WaitForExit(Process process, TimeSpan timeOut)
        {
            Output output;
            var ct = new CancellationTokenSource(timeOut).Token;
            try
            {
                await process.WaitForExitAsync(ct);
                if (process.ExitCode != 0)
                    output = await ReadError(process);
                else
                    output = await ReadOk(process);
            }
            catch (TaskCanceledException)
            {
                Kill(process, timeOut);
                output = ReadError(process, $"The process took more than {timeOut.Seconds} seconds");
            }

            while (!process.HasExited)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            return output;
        }

        private void Kill(Process process, TimeSpan timeOut)
        {
            if (!process.HasExited)
            {
                process.Kill(true);
            }

            // We synchronously wait for the process to complete.
            process.WaitForExit((int) timeOut.TotalMilliseconds);

            if (!process.HasExited)
            {
                throw new InvalidOperationException("Process not killed");
            }
        }

        private static async Task<Output> ReadError(Process process)
        {
            return ReadError(process, await process.StandardError.ReadToEndAsync());
        }

        private static Output ReadError(Process process, string message)
        {
            return Output.Error(process.ExitCode, message);
        }

        private static async Task<Output> ReadOk(Process process)
        {
            return Output.Ok(await process.StandardOutput.ReadToEndAsync());
        }
    }
}