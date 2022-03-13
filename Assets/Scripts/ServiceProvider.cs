using UnityEngine;

public interface IServiceLocator
{
    ITicketRequester TicketRequester { get; }
    ITicketRoadmapCreator TicketRoadmapCreator { get; }
}

public class ServiceLocator : IServiceLocator
{
    private static ServiceLocator _instance;
    private IServiceLocator _seviceProviderImplementation;

    public static ServiceLocator Instance => _instance ??= new ServiceLocator();

    private ServiceLocator()
    {
        TicketRequester = new MockTicketRequester();
        TicketRoadmapCreator = GameObject.FindObjectOfType<BaseTicketRoadmapCreator>();
    }

    public ITicketRequester TicketRequester { get; }
    public ITicketRoadmapCreator TicketRoadmapCreator { get; }
}