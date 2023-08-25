using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool CanInteract { get; }

    protected abstract UniTask OnInteract();

    public async UniTask Interact()
    {
        if (CanInteract)
        {
            await OnInteract();
        }
    }
}