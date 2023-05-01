namespace CompilerServiceAPI.Services
{
    public interface ICompileService
    {
        public string CompileCode(string code);
        public string LaunchCode();
    }
}
