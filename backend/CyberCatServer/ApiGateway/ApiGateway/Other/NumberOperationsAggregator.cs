namespace ApiGateway;

public class NumberOperationsAggregator
{
    private Guid _guid;
    private int _count;

    public NumberOperationsAggregator()
    {
        _guid = Guid.NewGuid();
    }
    public void Increment()
    {
        _count++;
    }

    public int GetCount()
    {
        return _count;
    }
}