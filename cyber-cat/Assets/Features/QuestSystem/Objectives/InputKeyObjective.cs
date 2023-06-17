using System;
using Features.QuestSystem.Actions;
using UnityEngine;

[Serializable]
public class InputKeyObjective : ActionStateableObjective
{
    [SerializeField] private KeyCode _key;

    private bool _isInput;

    public override bool IsComplete => _isInput;

    protected override void OnUpdate(float dt)
    {
        _isInput = Input.GetKeyDown(_key);
    }
}