using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class TaskNodeView : MonoBehaviour
{
    public Text NodeNumberOfTask;
    public Text NodeName;
    public Image NodeFrontImage;

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
}