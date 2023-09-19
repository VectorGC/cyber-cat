using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Conditional/Keyboard/", "Condition")]
public class IsKeyPressed : ConditionalAbort
{
    [SerializeField] private KeyCode _keyCode;

    public override bool Condition()
    {
        return Input.GetKey(_keyCode);
    }
}