using Cysharp.Threading.Tasks;

public class QuestGiver : Interactable
{
    public override bool CanInteract => true;

    protected override UniTask OnInteract()
    {
        return UniTask.CompletedTask;
    }
}