using System.Diagnostics;
using System.Text;
using CppLauncherService.Configurations;
using CppLauncherService.InternalModels;
using Microsoft.Extensions.Options;

namespace CppLauncherService.Services
{
    internal class ConsoleExecutorProxy : IProcessExecutorProxy
    {
        private readonly TimeSpan _timeOut;
        private readonly ILogger<ConsoleExecutorProxy> _logger;

        public ConsoleExecutorProxy(IOptions<CppLauncherAppSettings> appSettings, ILogger<ConsoleExecutorProxy> logger)
        {
            _logger = logger;
            _timeOut = appSettings.Value.ProcessTimeout;
        }

        public async Task<Output> Run(RunCommand command)
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
            if (output.HasError)
            {
                return output;
            }

            while (!process.HasExited)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            if (process.ExitCode != 0)
            {
                return await process.ReadError();
            }

            return await process.ReadOk();
        }

        private async Task<Output> WaitForExit(Process process, TimeSpan timeOut)
        {
            var ct = new CancellationTokenSource(timeOut).Token;
            try
            {
                await process.WaitForExitAsync(ct);
                return Output.Empty;
            }
            catch (TaskCanceledException)
            {
                Kill(process, timeOut);
                return process.ReadError($"The process took more than {timeOut.Seconds} seconds");
            }
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
    }
}