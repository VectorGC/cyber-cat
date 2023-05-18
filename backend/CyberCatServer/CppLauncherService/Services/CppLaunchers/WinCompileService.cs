using CompilerServiceAPI.InternalModels;

namespace CompilerServiceAPI.Services.CppLaunchers
{
    internal class WinCompileService : ICppLauncherService
    {
        private readonly IProcessExecutorProxy _processExecutorProxy;

        public WinCompileService(IProcessExecutorProxy processExecutorProxy)
        {
            _processExecutorProxy = processExecutorProxy;
        }

        public async Task<Output> CompileCode(string sourceCode)
        {
            using (StreamWriter writer = System.IO.File.CreateText("code.cpp"))
            {
                await writer.WriteAsync(sourceCode);
            }

            return await _processExecutorProxy.Run("wsl", "g++ code.cpp -Wall -Werror -o code -static-libgcc -static-libstdc++");
        }

        public async Task<Output> LaunchCode()
        {
            return await _processExecutorProxy.Run("wsl", "./code");
        }
    }
}