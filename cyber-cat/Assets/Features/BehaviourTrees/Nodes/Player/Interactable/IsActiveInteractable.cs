using System.Linq;
using System.Text;
using Bonsai;
using Bonsai.Core;
using UnityEngine;
using Zenject;

[BonsaiNode("Player/Interactable/")]
public class IsActiveInteractable : ConditionalAbort
{
    [SerializeField] private string _gameObjectName;

    private Interactable[] _interactables;
    private Interactable _target;

    [Inject]
    private void Construct(Interactable[] interactables)
    {
        _interactables = interactables;
        _target = _interactables.FirstOrDefault(go => go.gameObject.name == _gameObjectName);
    }

    public override bool Condition()
    {
        if (!_target)
        {
            return false;
        }

        return _target.gameObject.activeInHierarchy;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"GameObject '{_gameObjectName}'");
    }
}