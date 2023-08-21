using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual string InteractText { get; } = "Взаимодействовать F";
    public abstract bool CanInteract { get; }
    public abstract void OnInteract();

    public virtual void OnExitInteractableZone()
    {
    }
}