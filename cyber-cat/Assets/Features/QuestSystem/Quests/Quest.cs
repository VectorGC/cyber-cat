using System;
using UnityEngine;

[Serializable]
public class Quest : IQuest
{
    [SerializeReference, SubclassSelector] private IObjective _objective;

    public event Action<IQuest> Completed;

    public void Update(float dt)
    {
        _objective.Update(dt);
        if (_objective.IsComplete)
        {
            Completed?.Invoke(this);
        }
    }
}