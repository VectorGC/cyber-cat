public interface IObjective
{
    bool IsComplete { get; }
    void Update(float dt);
}