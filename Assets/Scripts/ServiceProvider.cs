using DefaultNamespace;
using UnityEngine;

public interface ISeviceProvider
{
    ITicketRequester TicketRequester { get; }
    ITicketRoadmapCreator TicketRoadmapCreator { get; }
}

public class ServiceProvider : ISeviceProvider
{
    private static ServiceProvider _instance;

    public static ServiceProvider Instance => _instance ??= new ServiceProvider();

    private ServiceProvider()
    {
        TicketRequester = new MockTicketRequester();
        TicketRoadmapCreator = GameObject.FindObjectOfType<BaseTicketRoadmapCreator>();
    }

    public ITicketRequester TicketRequester { get; }
    public ITicketRoadmapCreator TicketRoadmapCreator { get; }
}