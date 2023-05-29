namespace Shared.Models;

public interface ISolution
{
    string TaskId { get; }
    string SourceCode { get; }
}