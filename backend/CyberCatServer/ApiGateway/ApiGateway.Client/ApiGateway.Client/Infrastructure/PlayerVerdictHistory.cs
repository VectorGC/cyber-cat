using System;
using System.Collections.Generic;
using System.Linq;
using ApiGateway.Client.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Infrastructure
{
    public class PlayerVerdictHistory : IPlayerVerdictHistory
    {
        private readonly SortedList<DateTime, Verdict> _history = new SortedList<DateTime, Verdict>();

        public void Add(Verdict verdict, DateTime dateTime)
        {
            _history[dateTime] = verdict;
        }

        public Verdict GetLastVerdict(TaskId taskId)
        {
            var verdict = _history.FirstOrDefault(kvp => kvp.Value.TaskId.Equals(taskId));
            return verdict.Value;
        }
    }
}