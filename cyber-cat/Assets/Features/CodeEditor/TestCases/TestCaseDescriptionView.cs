using System.Text;
using ApiGateway.Client.Models;
using UniMob;
using UnityEngine;

public class TestCaseDescriptionView : LifetimeUIBehaviour<ConsoleState>
{
    [SerializeField] private SerializableInterface<IText> _text;

    [Atom] public override ConsoleState State { get; set; }

    protected override void OnUpdate()
    {
        var selectedVerdict = State?.GetSelectedTestCaseVerdict();
        if (selectedVerdict != null)
        {
            _text.Value.Text = GetDescription(selectedVerdict);
        }

        var selectedTestCase = State?.GetSelectedTestCase();
        if (selectedTestCase != null)
        {
            _text.Value.Text = GetDescription(selectedTestCase);
        }
    }

    private string GetDescription(ITestCase testCase)
    {
        var sb = new StringBuilder();
        if (testCase.Inputs.Length > 0)
        {
            sb.AppendLine("Вход:");
            var inputs = string.Join(" ", testCase.Inputs);
            sb.AppendLine($"`{inputs}`");
            sb.AppendLine();
        }

        sb.AppendLine("Ожидается:");
        sb.AppendLine($"`{testCase.Expected}`");

        return sb.ToString();
    }

    private string GetDescription(ITestCaseVerdict testCaseVerdict)
    {
        var testCase = testCaseVerdict.TestCase;

        var sb = new StringBuilder();
        if (testCase.Inputs.Length > 0)
        {
            sb.AppendLine("Вход:");
            var inputs = string.Join(" ", testCase.Inputs);
            sb.AppendLine($"`{inputs}`");
            sb.AppendLine();
        }

        sb.AppendLine("Вывод:");
        sb.AppendLine($"`{testCaseVerdict.Output}`");
        sb.AppendLine();

        sb.AppendLine("Ожидается:");
        sb.AppendLine($"`{testCase.Expected}`");

        return sb.ToString();
    }
}