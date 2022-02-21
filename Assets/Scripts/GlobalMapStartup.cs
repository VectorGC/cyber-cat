using UnityEngine;

public class GlobalMapStartup : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Hello Cyber Cat");

        var serviceProvider = ServiceLocator.Instance;
        var tickets = serviceProvider.TicketRequester.GetTickets();
        serviceProvider.TicketRoadmapCreator.CreateRoadmap(tickets);
        
        // Uncomment to test load window.
        //serviceProvider.LoadWindow.StartLoading();
    }
}
