namespace CppLauncherService.Services;

internal interface ICppFileCreator
{
    Task<string> CreateCppWithText(string sourceCode);
    string GetObjectFileName(string cppFileName);
}