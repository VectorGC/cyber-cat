using System;
using ApiGateway.Client.Models;

public interface ICodeEditor
{
    event Action Closed;
    bool IsOpen { get; }
    ITask Task { get; }
    void Open(ITask task);
    void Close();
}