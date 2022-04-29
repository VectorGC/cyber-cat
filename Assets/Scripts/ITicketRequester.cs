using System.Collections.Generic;

public interface ITaskTicket
{
    int Id { get; }
    string Name { get; }
    string Description { get; }
}

public class SimpleTaskTicket : ITaskTicket
{
    public int Id { get; }
    public int CountOfChildren { get; }
    public string Name { get; }
    public string Description { get; }

    public SimpleTaskTicket(int id, int countOfChildren, string ticketName, string ticketTheme)
    {
        Id = id;
        Name = ticketName;
        Description = ticketTheme;
        CountOfChildren = countOfChildren;
    }
}

public interface ITicketRequester
{
    IReadOnlyCollection<ITaskTicket> GetTickets();
}

public class MockTicketRequester : ITicketRequester
{
    public IReadOnlyCollection<ITaskTicket> GetTickets()
    {
        var tickets = new List<ITaskTicket>()
        {
            new SimpleTaskTicket(id: 23, countOfChildren: 1, ticketName: "Бюджетная команда", ticketTheme: "Green"),
            new SimpleTaskTicket(id: 24, countOfChildren: 2, ticketName: "Бюджетная команда #2", ticketTheme: "Green"),
            new SimpleTaskTicket(id: 25, countOfChildren: 0, ticketName: "Моё первое уничтожение", ticketTheme: "Blue"),
            new SimpleTaskTicket(id: 26, countOfChildren: 1, ticketName: "Нейродорожки", ticketTheme: "Blue"),
            new SimpleTaskTicket(id: 27, countOfChildren: 2, ticketName: "Нейродорожки 2", ticketTheme: "Blue"),
            new SimpleTaskTicket(id: 28, countOfChildren: 0, ticketName: "Нейродорожки 3", ticketTheme: "Green"),
            new SimpleTaskTicket(id: 29, countOfChildren: 0, ticketName: "Нейродорожки 4", ticketTheme: "Blue"),
        };

        return tickets;
    }
}