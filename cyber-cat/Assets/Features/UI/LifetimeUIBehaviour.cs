using UniMob;

public abstract class LifetimeUIBehaviour : LifetimeMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        Atom.Reaction(Lifetime, OnUpdate, debugName: this.GetType().Name);
    }

    protected abstract void OnUpdate();
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

    protected virtual void OnInitState(TState state)
    {
    }

    protected virtual void OnUpdate()
    {
    }
}