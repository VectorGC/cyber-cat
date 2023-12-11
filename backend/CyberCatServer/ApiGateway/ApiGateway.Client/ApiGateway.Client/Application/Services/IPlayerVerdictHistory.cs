using System;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Application.Services
{
    public interface IPlayerVerdictHistory
    {
        void Add(Verdict verdict, DateTime dateTime);
        Verdict GetLastVerdict(TaskId taskId);
    }
}