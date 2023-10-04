using System;

public interface IModal
{
    event Action Closed;
    bool IsShow { get; }
    void Show();
}