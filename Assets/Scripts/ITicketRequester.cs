using System.Collections.Generic;

namespace DefaultNamespace
{
    public interface ITicket
    {
        int Id { get; }
        string TicketName { get; }
        string TicketTheme { get; }
    }

    public class SimpleTicket : ITicket
    {
        public int Id { get; }
        public string TicketName { get; }
        public string TicketTheme { get; }

        public SimpleTicket(int id, string ticketName, string ticketTheme)
        {
            Id = id;
            TicketName = ticketName;
            TicketTheme = ticketTheme;
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
                new SimpleTicket(24, "Бюджетная команда", "Green"),
                new SimpleTicket(26, "Бюджетная команда #2", "Green"),
                new SimpleTicket(25, "Моё первое уничтожение", "Blue"),
                new SimpleTicket(28, "Нейродорожки", "Blue")
            };

            return tickets;
        }
    }
}