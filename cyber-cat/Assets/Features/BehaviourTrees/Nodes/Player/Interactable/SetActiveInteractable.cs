using System.Linq;
using System.Text;
using Bonsai;
using Bonsai.Core;
using UnityEngine;
using Zenject;

[BonsaiNode("Player/Interactable/")]
public class SetActiveInteractable : Task
{
    [SerializeField] private string _gameObjectName;
    [SerializeField] private bool _isActive;

    private Interactable[] _interactables;
    private Interactable _target;

    [Inject]
    private void Construct(Interactable[] interactables)
    {
        _interactables = interactables;
        _target = _interactables.FirstOrDefault(go => go.gameObject.name == _gameObjectName);
    }

    public override Status Run()
    {
        if (!_target)
        {
            return Status.Failure;
        }

        _target.gameObject.SetActive(_isActive);
        return Status.Success;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"Set GameObject '{_gameObjectName}' to '{_isActive}'");
    }
}