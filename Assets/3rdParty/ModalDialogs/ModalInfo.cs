using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Button;

[System.Serializable]
public class ModalInfo
{
    [SerializeField][Min(0)] public float TimeBeforeShow;
    [SerializeField][Multiline(1)] public string Title;
    [SerializeField][TextArea(1, 6)] public string Description;
    [SerializeField] public Sprite Icon;
    [SerializeField][Range(1, 3)] public int CountOfButtons;
    [SerializeField] public string[] ButtonText;
    [SerializeField] public ButtonClickedEvent[] ButtonEvents;
}
