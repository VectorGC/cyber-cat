using Shared.Models.Domain.Verdicts;

namespace PlayerService.Domain;

public class VerdictEntity
{
    public string TaskId { get; set; }
    public string Solution { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsSuccess { get; set; }

    public VerdictEntity(Verdict verdict)
    {
        TaskId = verdict.TaskId;
        Solution = verdict.Solution;
        DateTime = verdict.DateTime;
        IsSuccess = verdict.IsSuccess;
    }

    public VerdictEntity()
    {
    }
}