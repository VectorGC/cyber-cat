using System;

public interface IQuest
{
    event Action<IQuest> Completed;
    void Update(float dt);
}