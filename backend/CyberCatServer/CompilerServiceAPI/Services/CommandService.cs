using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerServiceAPI.Services
{
    public class CommandService : ICommandService
    {
        public CommandService() { }

        public string RunDockerCommand(string filename, string arguments)
        {
            //Аналогично, но с перегрузкой потока вывода
            ProcessStartInfo startInfo = new()
            {
                FileName = filename,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            Process process = new() { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                return process.StandardError.ReadToEnd();
            }
            else
                return process.StandardOutput.ReadToEnd();
        }

    }
}
