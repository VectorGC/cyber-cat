using ProtoBuf;

namespace Shared.Server.Data
{
    [ProtoContract]
    public class OutputDto
    {
        [ProtoMember(1)] public string StandardOutput { get; set; }
        [ProtoMember(2)] public string StandardError { get; set; }
        public bool Success => string.IsNullOrEmpty(StandardError);

        public static OutputDto Error(string error)
        {
            return new OutputDto()
            {
                StandardError = error
            };
        }
    }
}