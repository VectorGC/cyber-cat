using System;
using Shared.Models.Domain.Tasks;

public interface ICodeEditor
{
    event Action Closed;
    bool IsOpen { get; }
    TaskDescription Task { get; }
    void Open(TaskDescription task);
    void Close();
}