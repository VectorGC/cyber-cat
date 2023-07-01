using System;
using UnityEngine;

namespace Features.QuestSystem.Actions
{
    [Serializable]
    public abstract class ActionStateableObjective : StateableObjective
    {
        [SerializeReference, SubclassSelector] private IAction _onStart;
        [SerializeReference, SubclassSelector] private IAction _onComplete;

        protected sealed override void OnStart()
        {
            _onStart?.Execute();
        }

        protected sealed override void OnComplete()
        {
            _onComplete?.Execute();
        }
    }
}