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