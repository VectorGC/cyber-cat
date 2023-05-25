using ProtoBuf;

namespace Shared.Dto.ProtoHelpers;

[ProtoContract]
public class ListProto<T>
{
    [ProtoMember(1)] public List<T> Values { get; set; } = new();

    public List<T>.Enumerator GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    public static implicit operator List<T>(ListProto<T> proto)
    {
        return proto.Values;
    }

    public static implicit operator ListProto<T>(List<T> values)
    {
        return new ListProto<T>
        {
            Values = values
        };
    }
}

public static class ListProtoExtensions
{
    public static List<TSource> ToListProto<TSource>(this IEnumerable<TSource> source) where TSource : new()
    {
        if (source == null)
        {
            throw new ArgumentException(nameof(source));
        }

        return source as ListProto<TSource> ?? new ListProto<TSource>
        {
            Values = source as List<TSource> ?? new List<TSource>(source)
        };
    }
}