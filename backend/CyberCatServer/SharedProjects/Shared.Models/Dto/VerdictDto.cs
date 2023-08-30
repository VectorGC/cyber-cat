using ProtoBuf;
using Shared.Models.Enums;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class VerdictDto
    {
        [ProtoMember(1)]
        public VerdictStatus Status
        {
            get => status;
            set => status = value;
        }

        [ProtoMember(2)]
        public string Error
        {
            get => error;
            set => error = value;
        }

        [ProtoMember(3)]
        public int TestsPassed
        {
            get => testsPassed;
            set => testsPassed = value;
        }


        public VerdictStatus status;
        public string error;
        public int testsPassed;

        public override string ToString()
        {
            return $"{nameof(Status)}: {Status}";
        }
    }
}