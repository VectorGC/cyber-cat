using ProtoBuf;

namespace Shared.Server.ProtoHelpers;

[ProtoContract]
public class Args<T1>
{
    [ProtoMember(1)] public T1 Arg { get; set; }

    public static implicit operator Args<T1>(T1 value)
    {
        return new Args<T1>()
        {
            Arg = value
        };
    }

    public static implicit operator T1(Args<T1> args)
    {
        return args.Arg;
    }
}