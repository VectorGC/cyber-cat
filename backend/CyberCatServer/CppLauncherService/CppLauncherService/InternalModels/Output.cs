namespace CppLauncherService.InternalModels;

internal class Output
{
    public string StandardOutput { get; private init; }
    public string StandardError { get; private init; }
    public int? ExitCode { get; private init; }

    public bool HasError => !string.IsNullOrEmpty(StandardError);

    public static readonly Output Empty = new();

    public static Output Error(int exitCode, string message)
    {
        return new Output
        {
            ExitCode = exitCode,
            StandardError = $"Exit Code {exitCode}: {message}"
        };
    }

    public static Output Ok(string message)
    {
        return new Output
        {
            StandardOutput = message
        };
    }

    private Output()
    {
    }
}