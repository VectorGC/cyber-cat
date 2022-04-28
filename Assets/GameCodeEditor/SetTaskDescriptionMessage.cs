using TaskUnits;

public struct SetTaskDescriptionMessage
{
    public ITaskData TaskTicket { get; }

    public SetTaskDescriptionMessage(ITaskData taskTicket)
    {
        TaskTicket = taskTicket;
    }
}