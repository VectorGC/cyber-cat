using System.Collections.Generic;
using ProtoBuf;

namespace Shared.Models.Domain.Users
{
    [ProtoContract]
    public class Roles
    {
        public static Role Admin = new Role("Admin");
        public static Role Player = new Role("Player");

        [ProtoMember(1)] public HashSet<string> Values { get; set; } = new HashSet<string>();

        public bool Has(Role role)
        {
            return Values.Contains(role.Id);
        }
    }
}