using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialMenuButton : MonoBehaviour
{
    void Start()
    {
        TryGetComponent<Button>(out var button);
        //button.interactable = false;
    }
}