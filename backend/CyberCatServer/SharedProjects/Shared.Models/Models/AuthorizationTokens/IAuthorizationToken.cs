namespace Shared.Models.Models.AuthorizationTokens
{
    public interface IAuthorizationToken
    {
        string Type { get; }
        string Value { get; }
    }
}