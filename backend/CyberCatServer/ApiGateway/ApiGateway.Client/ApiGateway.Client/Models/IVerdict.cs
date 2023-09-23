namespace ApiGateway.Client.Models
{
    public interface IVerdict
    {
    }

    public interface IVerdictV2
    {
    }

    public class CompileError : IVerdictV2
    {
        public string Error { get; }
    }
}