using UniMob;
using Zenject;

public abstract class LifetimeUIBehaviour : LifetimeMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        Atom.Reaction(Lifetime, OnUpdate, debugName: this.GetType().Name);
    }

    protected abstract void OnUpdate();
}

public abstract class LifetimeUIBehaviourV2<TState> : LifetimeMonoBehaviour where TState : ILifetimeScope
{
    [Atom] protected abstract TState State { get; set; }

    [Inject]
    private void Construct(TState state)
    {
        State = state;
    }

    protected sealed override void Start()
    {
        base.Start();
        Atom.Reaction(Lifetime, OnUpdate, debugName: $"{GetType().Name}.{nameof(OnUpdate)}");
        OnInit();
    }

    protected sealed override void OnDestroy()
    {
        OnDispose();
        base.OnDestroy();
    }

    protected virtual void OnInit()
    {
    }

    protected virtual void OnDispose()
    {
    }

    protected virtual void OnUpdate()
    {
    }
}

public abstract class LifetimeUIBehaviour<TState> : LifetimeMonoBehaviour
{
    [Atom] public abstract TState State { get; set; }

    protected override void Start()
    {
        base.Start();
        Atom.Reaction(Lifetime, OnUpdate, debugName: $"{GetType().Name}.{nameof(OnUpdate)}");
        Atom.When(Lifetime, () => State != null, () => OnInitState(State), debugName: $"{GetType().Name}.{nameof(OnInitState)}({nameof(State)})");
    }

    private void Awake()
    {
        OnInitView();
    }

    protected override void OnDestroy()
    {
        if (State != null)
        {
            OnDisposeState(State);
        }

        OnDisposeView();
        base.OnDestroy();
    }

    protected virtual void OnInitView()
    {
    }

    protected virtual void OnDisposeView()
    {
    }

    protected virtual void OnInitState(TState state)
    {
    }

    protected virtual void OnDisposeState(TState state)
    {
    }

    protected virtual void OnUpdate()
    {
    }
}