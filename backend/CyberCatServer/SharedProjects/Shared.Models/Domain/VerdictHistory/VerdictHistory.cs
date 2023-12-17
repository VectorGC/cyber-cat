using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace Shared.Models.Domain.VerdictHistory
{
    public class VerdictHistory : IEnumerable<Verdict>
    {
        // Sorted by descending.
        private readonly SortedList<DateTime, Verdict> _history = new SortedList<DateTime, Verdict>(Comparer<DateTime>.Create((x, y) => x.CompareTo(y) * -1));

        public VerdictHistory(List<Verdict> verdicts)
        {
            AddRange(verdicts);
        }

        public void Add(Verdict verdict)
        {
            _history[verdict.DateTime] = verdict;
        }

        public void AddRange(IEnumerable<Verdict> verdicts)
        {
            foreach (var verdict in verdicts)
            {
                Add(verdict);
            }
        }

        public Verdict GetLastVerdict(TaskId taskId)
        {
            var verdict = _history.FirstOrDefault(kvp => kvp.Value.TaskId.Equals(taskId));
            return verdict.Value;
        }

        public IEnumerable<Verdict> GetBestOrLastVerdictForAllTasks()
        {
            var taskIds = new HashSet<TaskId>(_history.Values.Select(v => v.TaskId));
            foreach (var id in taskIds)
            {
                var verdict = GetBestOrLastVerdict(id);
                yield return verdict;
            }
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Verdict> GetEnumerator()
        {
            return _history.Values.GetEnumerator();
        }
    }
}