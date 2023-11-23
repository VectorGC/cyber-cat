namespace Shared.Models.Domain.Users
{
    public readonly struct Role
    {
        public string Id { get; }

        public Role(string id)
        {
            Id = id;
        }
    }
}