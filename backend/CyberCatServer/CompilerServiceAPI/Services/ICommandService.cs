namespace CompilerServiceAPI.Services
{
    public interface ICommandService
    {
        string RunDockerCommand(string filename, string arguments);
    }
}
