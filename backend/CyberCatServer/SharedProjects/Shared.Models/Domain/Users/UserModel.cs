namespace Shared.Models.Domain.Users
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public Roles Roles { get; set; }
    }
}