using System.Text;
using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
using Shared.Models.Ids;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TestCaseDescriptionView : UIBehaviour
{
    [SerializeField] private SerializableInterface<IText> _text;

    private ICodeEditor _codeEditor;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    public async UniTaskVoid SetTestCaseAsync(TestCaseId testCaseId)
    {
        var testCases = await _codeEditor.Task.GetTestCases();
        var testCase = testCases[testCaseId];

        _text.Value.Text = GetDescription(testCase);
    }

    private string GetDescription(ITestCase testCase)
    {
        var sb = new StringBuilder();
        if (testCase.Inputs.Length > 0)
        {
            sb.AppendLine("Вход: ");
            var inputs = string.Join(" ", testCase.Inputs);
            sb.AppendLine($"`{inputs}`");
            sb.AppendLine();
        }

        sb.AppendLine("Ожидается на выходе: ");
        sb.AppendLine($"`{testCase.Expected}`");

        return sb.ToString();
    }
}