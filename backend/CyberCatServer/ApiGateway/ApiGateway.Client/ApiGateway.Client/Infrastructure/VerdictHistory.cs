using System;
using System.Collections.Generic;
using System.Linq;
using ApiGateway.Client.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Infrastructure
{
    public class VerdictHistory : IVerdictHistory
    {
        // Sorted by descending.
        private readonly SortedList<DateTime, Verdict> _history = new SortedList<DateTime, Verdict>(Comparer<DateTime>.Create((x, y) => x.CompareTo(y) * -1));

        public void Add(Verdict verdict, DateTime dateTime)
        {
            _history[dateTime] = verdict;
        }

        public Verdict GetLastVerdict(TaskId taskId)
        {
            var verdict = _history.FirstOrDefault(kvp => kvp.Value.TaskId.Equals(taskId));
            return verdict.Value;
        }

        public Verdict GetBestOrLastVerdict(TaskId taskId)
        {
            var verdicts = _history.Values
                .Where(x => x.TaskId.Equals(taskId))
                .ToList();

            var success = verdicts.FirstOrDefault(x => x.IsSuccess);
            if (success != null)
                return success;

            return GetLastVerdict(taskId);
        }

        public void Clear()
        {
            _history.Clear();
        }
    }
}