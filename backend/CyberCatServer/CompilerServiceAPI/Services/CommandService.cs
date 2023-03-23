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
        public void RunDockerCommand(string filename, string arguments)
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = filename,
                Arguments = arguments,
                UseShellExecute = false, //Import in Linux environments
            };
            Process process = new() { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
        }
    }
}
