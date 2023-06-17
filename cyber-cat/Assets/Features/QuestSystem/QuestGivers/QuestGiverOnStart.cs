using Features.QuestSystem.Actions;
using UnityEngine;

public class QuestGiverOnStart : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private IAction _onStart;
    [SerializeReference, SubclassSelector] private IQuest _quest;

    private void Start()
    {
        _onStart?.Execute();
        QuestSystemFacade.QuestSystem.AddQuest(_quest);
    }
}