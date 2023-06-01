using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Shared.Dto.ProtoHelpers
{
    [ProtoContract]
    public class ListProto<TSource>
    {
        [ProtoMember(1)] public List<TSource> Values { get; set; } = new List<TSource>();

        public ListProto(IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentException(nameof(source));
            }

            Values = source as List<TSource> ?? new List<TSource>(source);
        }

        public ListProto()
        {
        }

        public List<TSource>.Enumerator GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        public static implicit operator List<TSource>(ListProto<TSource> proto)
        {
            return proto.Values;
        }

        public static implicit operator ListProto<TSource>(List<TSource> values)
        {
            return new ListProto<TSource>
            {
                Values = values
            };
        }
    }
}