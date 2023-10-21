namespace ApiGateway.Client.V2
{
    public interface IRole
    {
        T Access<T>() where T : class, IAccess;
        void Dispose();
    }
}