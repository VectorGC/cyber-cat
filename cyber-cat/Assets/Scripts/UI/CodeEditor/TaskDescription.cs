using Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TaskDescription : UIBehaviour
    {
        [SerializeField] private TextField goalTask;
        [SerializeField] private TextField descriptionTask;

        public ITask Task
        {
            set => SetTask(value);
        }

        private void SetTask(ITask task)
        {
            goalTask.Text = task.Name;
            descriptionTask.Text = task.Description;
        }
    }
}