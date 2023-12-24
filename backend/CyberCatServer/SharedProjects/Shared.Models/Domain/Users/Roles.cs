using System.Collections;
using System.Collections.Generic;
using ProtoBuf;

namespace Shared.Models.Domain.Users
{
    [ProtoContract]
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Player = "Player";

        [ProtoMember(1)] public List<string> Values { get; set; } = new List<string>();

        public Roles(IEnumerable<string> roles)
        {
            Values.AddRange(roles);
        }

        public Roles(string role)
        {
            Values.Add(role);
        }

        public Roles()
        {
        }

        public void Add(string roleId)
        {
            Values.Add(roleId);
        }

        public bool Has(string roleId)
        {
            return Values.Contains(roleId);
        }
    }
}