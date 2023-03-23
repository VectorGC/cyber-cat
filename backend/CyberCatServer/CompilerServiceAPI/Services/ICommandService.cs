namespace CompilerServiceAPI.Services
{
    public interface ICommandService
    {
        void RunDockerCommand(string filename, string arguments);
    }
}
