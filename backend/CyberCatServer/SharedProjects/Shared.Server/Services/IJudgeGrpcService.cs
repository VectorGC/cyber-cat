using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto.Data;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IJudgeGrpcService
{
    Task<Response<VerdictData>> GetVerdict(GetVerdictArgs args);
}