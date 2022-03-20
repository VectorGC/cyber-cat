using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using TasksData.Requests;
using TMPro;
using UniRx;
using UnityEngine;
using WebRequests.Requesters;

public class GoalObserver : MonoBehaviour
{
    [SerializeField] private TMP_Text goalText;
    
    // Start is called before the first frame update
    void Start()
    {
        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(10))
            .Do(x => SendRequest())
            .Subscribe();
    }

    private void SendRequest()
    {
        new GetTasksRequest(TokenSession.FromPlayerPrefs())
            .SendWWWGetObject()
            .Subscribe(OnTasksReceived);
    }

    private void OnTasksReceived(TasksData.TasksData tasksData)
    {
        var completedTasks = tasksData.Tasks.Count(task => task.Value.IsPassed);
        SetGoalText(completedTasks, tasksData.Count);
    }

    private void SetGoalText(int completedTasksCount, int tasksCount)
    {
        goalText.text = $"Выполнено: {completedTasksCount} / {tasksCount}";
    }
}
