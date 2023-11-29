using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application;
using ApiGateway.Client.V3.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Ids;

namespace ApiGateway.Client.V3.Infrastructure
{
    public class TaskWebRepository : ITaskRepository, IDisposable
    {
        private readonly Dictionary<TaskId, TaskModel> _tasks = new Dictionary<TaskId, TaskModel>();

        private readonly PlayerService _playerService;
        private readonly TaskDescriptionWebProvider _descriptionProvider;
        private readonly TaskPlayerProgressWebProvider _dataProvider;

        public TaskWebRepository(PlayerService playerService, TaskDescriptionWebProvider descriptionProvider, TaskPlayerProgressWebProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _descriptionProvider = descriptionProvider;
            _playerService = playerService;

            _playerService.OnPlayerLogined += OnPlayerLogined;
        }

        public void Dispose()
        {
            _playerService.OnPlayerLogined -= OnPlayerLogined;
        }

        private async Task OnPlayerLogined(PlayerContext playerContext)
        {
            var descriptions = await _descriptionProvider.GetTaskDescriptions(playerContext.Token);
            var progressDataCollection = await _dataProvider.GetTasksProgress(playerContext.Token);
            foreach (var description in descriptions)
            {
                var taskProgress = progressDataCollection.FirstOrDefault(data => data.TaskId == description.Id) ?? new TaskProgressData();
                _tasks[description.Id] = new TaskModel(description, taskProgress);
            }
        }

        public Dictionary<TaskId, TaskModel>.Enumerator GetEnumerator()
        {
            return _tasks.GetEnumerator();
        }


        IEnumerator<KeyValuePair<TaskId, TaskModel>> IEnumerable<KeyValuePair<TaskId, TaskModel>>.GetEnumerator()
        {
            return _tasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _tasks).GetEnumerator();
        }

        public int Count => _tasks.Count;

        public bool ContainsKey(TaskId key)
        {
            return _tasks.ContainsKey(key);
        }

        public bool TryGetValue(TaskId key, out TaskModel value)
        {
            return _tasks.TryGetValue(key, out value);
        }

        public TaskModel this[TaskId key] => _tasks[key];

        public IEnumerable<TaskId> Keys => ((IReadOnlyDictionary<TaskId, TaskModel>) _tasks).Keys;

        public IEnumerable<TaskModel> Values => ((IReadOnlyDictionary<TaskId, TaskModel>) _tasks).Values;
    }
}