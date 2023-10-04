using System;
using System.IO;
using ProtoBuf;

namespace Shared.Models.ProtoHelpers
{
    public static class ProtobufExtensions
    {
        public static string ToProtobufBytesString(this object model)
        {
            string bytesString;
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, model);
                bytesString = Convert.ToBase64String(stream.GetBuffer(), 0, (int) stream.Length);
            }

            return bytesString;
        }

        public static T ToProtobufObject<T>(this string bytesString)
        {
            T model;
            var byteAfter64 = Convert.FromBase64String(bytesString);
            using (var stream = new MemoryStream(byteAfter64))
            {
                model = Serializer.Deserialize<T>(stream);
            }

            return model;
        }
    }
}