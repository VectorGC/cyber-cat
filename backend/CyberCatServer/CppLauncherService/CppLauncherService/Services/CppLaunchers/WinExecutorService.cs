using CppLauncherService.InternalModels;

namespace CppLauncherService.Services.CppLaunchers
{
    internal class WinExecutorService : ICppExecutorOsSpecificService
    {
        private readonly IProcessExecutorProxy _processExecutorProxy;
        private readonly ICppFileCreator _cppFileCreator;

        public WinExecutorService(IProcessExecutorProxy processExecutorProxy, ICppFileCreator cppFileCreator)
        {
            _processExecutorProxy = processExecutorProxy;
            _cppFileCreator = cppFileCreator;
        }

        public async Task<CompileCppResult> CompileCode(string sourceCode)
        {
            var cppFileName = await _cppFileCreator.CreateCppWithText(sourceCode);
            var objectFileName = _cppFileCreator.GetObjectFileName(cppFileName);

            var output = await _processExecutorProxy.Run("wsl", $"g++ -g {cppFileName} -Wall -o {objectFileName} -static-libgcc -static-libstdc++");
            return new CompileCppResult
            {
                Output = output,
                ObjectFileName = objectFileName
            };
        }

        public async Task<Output> LaunchCode(string objectFileName, string input)
        {
            return await _processExecutorProxy.Run("wsl", $"./{objectFileName}", input);
        }
    }
}