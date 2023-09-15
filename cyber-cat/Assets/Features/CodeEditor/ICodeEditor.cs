using ApiGateway.Client.Models;

public interface ICodeEditor
{
    ITask Task { get; }
    void Close();
}