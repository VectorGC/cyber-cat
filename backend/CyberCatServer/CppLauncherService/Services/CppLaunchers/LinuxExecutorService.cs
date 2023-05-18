using CppLauncherService.InternalModels;

namespace CppLauncherService.Services.CppLaunchers
{
    internal class LinuxExecutorService : ICppExecutorOsSpecificService
    {
        private readonly IProcessExecutorProxy _processExecutorProxy;
        private readonly ICppFileCreator _cppFileCreator;

        public LinuxExecutorService(IProcessExecutorProxy processExecutorProxy, ICppFileCreator cppFileCreator)
        {
            _processExecutorProxy = processExecutorProxy;
            _cppFileCreator = cppFileCreator;
        }

        public async Task<CompileCppResult> CompileCode(string sourceCode)
        {
            var cppFileName = await _cppFileCreator.CreateCppWithText(sourceCode);
            var objectFileName = _cppFileCreator.GetObjectFileName(cppFileName);

            var pathToFile = await _cppFileCreator.CreateCppWithText(sourceCode);
            var output = await _processExecutorProxy.Run("g++", $"{pathToFile} -Wall -Werror -o code -static-libgcc -static-libstdc++");

            return new CompileCppResult
            {
                Output = output,
                ObjectFileName = objectFileName
            };
        }

        public async Task<Output> LaunchCode(string objectFileName)
        {
            return await _processExecutorProxy.Run($"{objectFileName}", string.Empty);
        }
    }
}