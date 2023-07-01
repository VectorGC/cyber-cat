﻿using System.Diagnostics;
using System.Text;
using CppLauncherService.InternalModels;
using Microsoft.Extensions.Options;

namespace CppLauncherService.Services
{
    internal class ConsoleExecutorProxy : IProcessExecutorProxy
    {
        private readonly TimeSpan _timeOut;

        public ConsoleExecutorProxy(IOptions<CppLauncherAppSettings> appSettings)
        {
            _timeOut = appSettings.Value.ProcessTimeout;
        }

        public async Task<Output> Run(RunCommand command)
        {
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
            await process.StandardInput.WriteAsync(command.Input);
            // После ввода нужно передать символ \n (как будто нажали Enter), чтобы программа засчитала ввод.
            await process.StandardInput.WriteAsync(Environment.NewLine);

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

            // Синхронно ждем завершения процесса.
            process.WaitForExit((int) timeOut.TotalMilliseconds);

            if (!process.HasExited)
            {
                throw new InvalidOperationException("Process not killed");
            }
        }
    }
}