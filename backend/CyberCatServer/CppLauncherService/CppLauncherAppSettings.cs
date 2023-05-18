namespace CppLauncherService;

public class CppLauncherAppSettings
{
    public int ProcessTimeoutSec { get; set; }

    public TimeSpan ProcessTimeout => TimeSpan.FromSeconds(ProcessTimeoutSec);
}