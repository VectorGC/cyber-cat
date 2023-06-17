public abstract class StateableObjective : IObjective
{
    public ObjectiveState State { get; private set; }

    public abstract bool IsComplete { get; }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnComplete()
    {
    }

    protected virtual void OnUpdate(float dt)
    {
    }

    void IObjective.Update(float dt)
    {
        switch (State)
        {
            case ObjectiveState.Inactive:
                State = ObjectiveState.Active;
                OnStart();
                break;
            case ObjectiveState.Active:
                OnUpdate(dt);
                if (IsComplete)
                {
                    State = ObjectiveState.Complete;
                    OnComplete();
                }

                break;
        }
    }
}