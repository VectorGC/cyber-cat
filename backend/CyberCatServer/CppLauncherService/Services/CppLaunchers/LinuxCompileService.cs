using CompilerServiceAPI.InternalModels;

namespace CompilerServiceAPI.Services.CppLaunchers
{
    internal class LinuxCompileService : ICppLauncherService
    {
        private readonly IProcessExecutorProxy _processExecutorProxy;

        public LinuxCompileService(IProcessExecutorProxy processExecutorProxy)
        {
            _processExecutorProxy = processExecutorProxy;
        }

        public async Task<Output> CompileCode(string sourceCode)
        {
            using (StreamWriter writer = System.IO.File.CreateText("code.cpp"))
            {
                await writer.WriteAsync(sourceCode);
            }

            return await _processExecutorProxy.Run("g++", "code.cpp -Wall -Werror -o code -static-libgcc -static-libstdc++");
        }

        public async Task<Output> LaunchCode()
        {
            return await _processExecutorProxy.Run("code", "");
        }
    }
}