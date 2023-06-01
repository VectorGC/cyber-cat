using System;

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