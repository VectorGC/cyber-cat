using System.Text;
using ApiGateway.Client.Models;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestCaseDescriptionView : UIBehaviour
{
    [SerializeField] private SerializableInterface<IText> _text;

    public void SetTestCase(ITestCase testCase)
    {
        _text.Value.Text = GetDescription(testCase);
    }

    public void SetTestCase(ITestCaseVerdict testCase)
    {
        _text.Value.Text = GetDescription(testCase);
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