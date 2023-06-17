using UnityEngine;

public class CharacterQuestGiver : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private IQuest _quest;
}