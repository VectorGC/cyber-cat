using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class TaskDescription : UIBehaviour
    {
        [SerializeField] private Text goalTask;
        [SerializeField] private Text descriptionTask;

        public ITask Task
        {
            set => SetTask(value);
        }

        private void SetTask(ITask task)
        {
            StartCoroutine(task.GetName().AsUniTask().ContinueWith(name => goalTask.text = name).ToCoroutine());
            StartCoroutine(task.GetDescription().AsUniTask().ContinueWith(description => descriptionTask.text = description).ToCoroutine());
        }
    }
}