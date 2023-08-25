using System.Text;
using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("UI/", "Hint")]
public class Hint : Task
{
    [SerializeField] private string _hint = "Взаимодействовать - F";

    private HUDController _hudController;

    public override void OnEnter()
    {
        _hudController = FindObjectOfType<HUDController>();
    }

    public override Status Run()
    {
        _hudController.HintText = _hint;
        return Status.Success;
    }
    
    public override void Description(StringBuilder builder)
    {
        base.Description(builder);
        builder.AppendLine(_hint);
    }
}