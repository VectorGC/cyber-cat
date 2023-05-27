using CppLauncherService.InternalModels;

namespace CppLauncherService.Services;

internal class CppErrorFormatService : ICppErrorFormatService
{
    public Output Format(Output source)
    {
        switch (source.ExitCode.GetValueOrDefault())
        {
            case 11: return Output.Error(11, "(SIGSEGV signal) Segmentation fault");
            default: return source;
        }
    }
}