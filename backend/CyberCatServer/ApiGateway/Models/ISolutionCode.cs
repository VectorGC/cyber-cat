namespace ApiGateway.Models;

public interface ISolutionCode
{
    UserId Author { get; }
    string TaskId { get; }
    string SourceCode { get; }
}