using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool CanInteract { get; }

    protected abstract void OnInteract();

    public void Interact()
    {
        if (CanInteract)
        {
            OnInteract();
        }
    }
}

public abstract class InteractableAsync : Interactable
{
    protected abstract UniTaskVoid OnInteractAsync();
    protected override void OnInteract()
    {
        OnInteractAsync().Forget();
    }
}