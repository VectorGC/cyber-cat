using System.Collections.Generic;
using UnityEngine;

public class QuestSystemBehaviour : MonoBehaviour, IQuestSystem
{
    private readonly List<IQuest> _quests = new List<IQuest>();

    public static QuestSystemBehaviour Create()
    {
        var gameObject = new GameObject(nameof(QuestSystemBehaviour));
        var questSystem = gameObject.AddComponent<QuestSystemBehaviour>();
        Instantiate(gameObject, Vector3.zero, Quaternion.identity);

        DontDestroyOnLoad(gameObject);

        return questSystem;
    }

    private QuestSystemBehaviour()
    {
    }

    public void AddQuest(IQuest quest)
    {
        _quests.Add(quest);
        quest.Completed += RemoveQuest;
    }

    private void RemoveQuest(IQuest quest)
    {
        quest.Completed -= RemoveQuest;
        _quests.Remove(quest);
    }

    private void Update()
    {
        foreach (var quest in _quests)
        {
            quest.Update(Time.deltaTime);
        }
    }
}