using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class OneMessageTrigger : Trigger
{
    [SerializeField] private Transform _position;
    [Range(1, 3)] [SerializeField] private int _countOfButtons;
    [SerializeField] private string _title;
    [TextArea(1, 6)] [SerializeField] private string _description;
    [SerializeField] private string[] _buttonsText;
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityAction[] actions1;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        transform.position = _position.position;
    }

    public override void Enter()
    {
        OnEnter?.Invoke();
        UnityAction[] actions = new UnityAction[]
        {

        };
        ModalPanel.Instance.MessageBos(null, _title, _description, true, _countOfButtons, actions, _buttonsText);
    }
}
