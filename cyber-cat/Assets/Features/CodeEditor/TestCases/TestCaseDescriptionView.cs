using System.Text;
using Shared.Models.Models.TestCases;
using Shared.Models.Models.Verdicts;
using UniMob;
using UnityEngine;

public class TestCaseDescriptionView : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private SerializableInterface<IText> _text;

    [Atom] public override CodeEditorState State { get; set; }

    protected override void OnUpdate()
    {
        var selectedVerdict = State?.GetSelectedTestCaseVerdict();
        var selectedTestCase = State?.GetSelectedTestCase();
        if (selectedVerdict != null)
        {
            _text.Value.Text = GetDescription(selectedVerdict);
        }
        else if (selectedTestCase != null)
        {
            _text.Value.Text = GetDescription(selectedTestCase);
        }
        else if (State?.GetVerdict() is NativeFailure nativeFailure)
        {
            _text.Value.Text = GetNativeFailureDescription(nativeFailure);
        }
        else
        {
            _text.Value.Text = string.Empty;
        }
    }

    private string GetDescription(TestCase testCase)
    {
        var sb = new StringBuilder();
        if (testCase.Inputs?.Length > 0)
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

    private string GetDescription(TestCaseVerdict testCaseVerdict)
    {
        var testCase = testCaseVerdict.TestCase;

        var sb = new StringBuilder();
        if (testCase.Inputs?.Length > 0)
        {
            sb.AppendLine("Вход:");
            var inputs = string.Join(" ", testCase.Inputs);
            sb.AppendLine($"`{inputs}`");
            sb.AppendLine();
        }

        sb.AppendLine("Ожидается:");
        sb.AppendLine($"`{testCase.Expected}`");

        sb.AppendLine("Ваш вывод:");
        sb.AppendLine($"`{testCaseVerdict.Output}`");
        sb.AppendLine();

        if (testCaseVerdict is FailureTestCaseVerdict failureTestCaseVerdict)
        {
            sb.AppendLine($"Ожидаем получить `{testCase.Expected}`, но получили `{failureTestCaseVerdict.Output}`");
        }

        return sb.ToString();
    }

    private string GetNativeFailureDescription(NativeFailure nativeFailure)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Возникла ошибка во время выполнения:");
        sb.AppendLine($"`{nativeFailure.Error}`");

        return sb.ToString();
    }
}