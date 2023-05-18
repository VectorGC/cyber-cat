using CompilerServiceAPI.InternalModels;

namespace CompilerServiceAPI.Services.CppLaunchers
{
    internal class WinCompileService : ICppLauncherService
    {
        private readonly IProcessExecutorProxy _processExecutorProxy;
        private readonly ICppFileCreator _cppFileCreator;

        public WinCompileService(IProcessExecutorProxy processExecutorProxy, ICppFileCreator cppFileCreator)
        {
            _processExecutorProxy = processExecutorProxy;
            _cppFileCreator = cppFileCreator;
        }

        public async Task<CompileCppResult> CompileCode(string sourceCode)
        {
            var cppFileName = await _cppFileCreator.CreateCppWithText(sourceCode);
            var objectFileName = _cppFileCreator.GetObjectFileName(cppFileName);

            var output = await _processExecutorProxy.Run("wsl", $"g++ {cppFileName} -Wall -Werror -o {objectFileName} -static-libgcc -static-libstdc++");
            return new CompileCppResult
            {
                Output = output,
                ObjectFileName = objectFileName
            };
        }

        public async Task<Output> LaunchCode(string objectFileName)
        {
            return await _processExecutorProxy.Run("wsl", $"./{objectFileName}");
        }
    }
}