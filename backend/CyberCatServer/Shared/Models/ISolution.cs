namespace Shared.Models;

public interface ISolution
{
    public string UserId { get; }
    public string TaskId { get; }
    string SourceCode { get; }
}