using System.Linq;
using System.Text;
using Bonsai;
using Bonsai.Core;
using UnityEngine;
using Zenject;

[BonsaiNode("Player/Interactable/", "Condition")]
public class CanInteract : ConditionalAbort
{
    [SerializeField] private string _gameObjectName;

    private Interactable[] _interactables;
    private Interactable _target;
    private Player _player;

    [Inject]
    private void Construct(Interactable[] interactables, Player player)
    {
        _player = player;
        _interactables = interactables;
        _target = _interactables.FirstOrDefault(go => go.gameObject.name == _gameObjectName);
    }

    public override bool Condition()
    {
        if (!_target)
        {
            return false;
        }

        return _player.Interact.CanInteract(_target);
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine($"GameObject '{_gameObjectName}'");
    }
}