using CppLauncherService.InternalModels;

namespace CppLauncherService.Services;

internal interface ICppErrorFormatService
{
    Output Format(Output source);
}