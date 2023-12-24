namespace CppLauncherService.Domain;

public readonly struct ConsoleCommand
{
    public string Command { get; }
    public string Arguments { get; }
    public string Input { get; }

    public ConsoleCommand(string command, string arguments, string input = null)
    {
        Command = command;
        Arguments = arguments;
        Input = input;
    }

    public override string ToString()
    {
        var log = $"{Command} {Arguments}";
        if (!string.IsNullOrEmpty(Input))
        {
            log = $"{log}, {nameof(Input)}: {Input}";
        }

        return log;
    }
}