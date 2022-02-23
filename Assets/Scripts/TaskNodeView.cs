using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TaskNodeView : MonoBehaviour
{
    public Text NodeNumberOfTask;
    public Text NodeName;
    public Image NodeFrontImage;
    public List<Image> stars;

    private ITaskTicket _ticket;
    public void OnClick()
    {
        var taskNodesController = FindObjectOfType<BaseTaskNodesController>();
        taskNodesController.OpenTask(_ticket);
    }

    public void SetupView(ITaskTicket ticket)
    {
        _ticket = ticket;
        NodeNumberOfTask.text = ticket.Id.ToString();
        NodeName.text = ticket.Name;
        NodeFrontImage.color = GetColorByTicketTheme(ticket);
    }

    private Color GetColorByTicketTheme(ITaskTicket ticket)
    {
        switch (ticket.Description)
        {
            case "Green": return Color.green;
            case "Blue": return Color.blue;
            default: return Color.magenta;
        }
    }

    public void LoadTask()
    {
        Debug.Log($"���� ������� ������ #{_ticket.Id} '{_ticket.Name}' ");
        foreach(var star in stars)
        {
            star.color = Color.yellow;
        }

        SceneManager.LoadScene("Code_editor_Blue");
        //Debug.Log($"#{_ticket.Id} '{_ticket.TicketName}' ������");
    }

}