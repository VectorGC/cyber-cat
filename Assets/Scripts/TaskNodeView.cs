using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskNodeView : MonoBehaviour
{
    public Text NodeNumberOfTask;
    public Text NodeName;
    public Image NodeFrontImage;
    public List<Image> stars;
    public Button NodeButton;

    private ITicket _ticket;

    public void UpdateView(ITicket ticket)
    {
        _ticket = ticket;
        NodeNumberOfTask.text = ticket.Id.ToString();
        NodeName.text = ticket.TicketName;
        NodeFrontImage.color = GetColorByTicketTheme(ticket);
    }

    private Color GetColorByTicketTheme(ITicket ticket)
    {
        switch (ticket.TicketTheme)
        {
            case "Green": return Color.green;
            case "Blue": return Color.blue;
            default: return Color.magenta;
        }
    }

    public void LoadTask()
    {
        Debug.Log($"Была выбрана задача #{_ticket.Id} '{_ticket.TicketName}' ");
        foreach(var star in stars)
        {
            star.color = Color.yellow;
        }
        Debug.Log($"#{_ticket.Id} '{_ticket.TicketName}' решена");
    }

}