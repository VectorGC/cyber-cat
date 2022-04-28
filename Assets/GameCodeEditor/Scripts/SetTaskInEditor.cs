using TasksData;
using TaskUnits;

public readonly struct SetTaskInEditor
{
    public ITaskData TaskTicket { get; }
    public string LastSavedCode { get; }

    public SetTaskInEditor(ITaskData taskTicket, string lastSavedCode)
    {
        TaskTicket = taskTicket;
        LastSavedCode = lastSavedCode;
    }
}