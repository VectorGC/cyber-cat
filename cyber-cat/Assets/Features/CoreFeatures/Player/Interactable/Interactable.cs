using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool CanInteract { get; }
    public abstract IEnumerator Interact();
}