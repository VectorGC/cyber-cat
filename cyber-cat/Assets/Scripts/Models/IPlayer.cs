namespace Models
{
    public interface IPlayer
    {
        string Name { get; }
        ITokenSession Token { get; }
    }
}