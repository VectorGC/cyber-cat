using System;
using UniRx;
using UnityEngine;

public class InputInAnimatorState : MonoBehaviour, IObservable<KeyCode>
{
    [SerializeField] private Animator animator;
    [SerializeField] private string observeAnyKeyInAnimatorState = "NextLabelHighlight";

    private readonly Subject<KeyCode> _subject = new Subject<KeyCode>();

    private void OnGUI()
    {
        var ev = Event.current;
        var isInputKey = ev is {isKey: true};
        if (!isInputKey)
        {
            return;
        }

        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(observeAnyKeyInAnimatorState) && !animator.IsInTransition(0))
        {
            _subject.OnNext(ev.keyCode);
            _subject.OnCompleted();
        }
    }

    public IDisposable Subscribe(IObserver<KeyCode> observer) => _subject.Subscribe(observer);
}