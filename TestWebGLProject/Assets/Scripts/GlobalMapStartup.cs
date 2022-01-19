using System;
using UnityEngine;

public class GlobalMapStartup : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Hello Cyber Cat");

        var serviceProvider = ServiceProvider.Instance;
        var tickets = serviceProvider.TicketRequester.GetTickets();
        serviceProvider.TicketRoadmapCreator.CreateRoadmap(tickets);
    }
}
