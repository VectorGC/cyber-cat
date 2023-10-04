using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Data;
using Shared.Models.Models;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IJudgeGrpcService
{
    Task<Response<VerdictData>> GetVerdict(GetVerdictArgs args);
    Task<Response<VerdictV2>> GetVerdictV2(GetVerdictArgs args);
}