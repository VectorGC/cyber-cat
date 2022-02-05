using System.Collections.Generic;

namespace DefaultNamespace
{
    public interface ITicket
    {
        int Id { get; }
        int CountOfChildren { get; }
        string TicketName { get; }
        string TicketTheme { get; }
    }

    public class SimpleTicket : ITicket
    {
        public int Id { get; }
        public int CountOfChildren { get; }
        public string TicketName { get; }
        public string TicketTheme { get; }

        public SimpleTicket(int id, int countOfChildren, string ticketName, string ticketTheme)
        {
            Id = id;
            TicketName = ticketName;
            TicketTheme = ticketTheme;
            CountOfChildren = countOfChildren;
        }
    }

    public interface ITicketRequester
    {
        IReadOnlyCollection<ITicket> GetTickets();
    }

    public class MockTicketRequester : ITicketRequester
    {
        public IReadOnlyCollection<ITicket> GetTickets()
        {
            var tickets = new List<ITicket>()
            {
                new SimpleTicket(id: 24, countOfChildren: 1, ticketName: "Бюджетная команда", ticketTheme: "Green"),
                new SimpleTicket(id: 26, countOfChildren: 2, ticketName: "Бюджетная команда #2", ticketTheme: "Green"),
                new SimpleTicket(id: 25, countOfChildren: 1, ticketName: "Моё первое уничтожение", ticketTheme: "Blue"),
                new SimpleTicket(id: 28, countOfChildren: 1, ticketName: "Нейродорожки", ticketTheme: "Blue")
            };

            return tickets;
        }
    }
}