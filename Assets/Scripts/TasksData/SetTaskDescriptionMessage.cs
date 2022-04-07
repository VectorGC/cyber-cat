public struct SetTaskDescriptionMessage
{
    public ITaskTicket TaskTicket { get; }

    public SetTaskDescriptionMessage(ITaskTicket taskTicket)
    {
        TaskTicket = taskTicket;
    }
}