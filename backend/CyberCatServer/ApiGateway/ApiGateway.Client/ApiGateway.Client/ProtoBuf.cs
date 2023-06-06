using System;

// ReSharper disable once CheckNamespace
namespace ProtoBuf
{
    public class ProtoContractAttribute : Attribute
    {
    }

    public class ProtoMember : Attribute
    {
        public ProtoMember(int order)
        {
        }
    }
}