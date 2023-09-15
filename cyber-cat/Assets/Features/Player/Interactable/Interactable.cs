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