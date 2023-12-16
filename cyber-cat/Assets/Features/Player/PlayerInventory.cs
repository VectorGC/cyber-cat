using System;
using ApiGateway.Client.Application;

public enum InventoryItem
{
    Antivirus = 0
}

public class PlayerInventory
{
    private readonly ApiGatewayClient _client;

    public PlayerInventory(ApiGatewayClient client)
    {
        _client = client;
    }

    public bool Has(InventoryItem item)
    {
        switch (item)
        {
            case InventoryItem.Antivirus:
                return IsSolvedTask(TaskType.Task3);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(item), item, null);
        }
    }

    private bool IsSolvedTask(TaskType taskType)
    {
        if (_client.Player == null)
        {
            var verdict = _client.VerdictHistory.GetBestOrLastVerdict(taskType.Id());
            return verdict?.IsSuccess ?? false;
        }

        var task = _client.Player.Tasks[taskType.Id()];
        return task.IsComplete;
    }
}