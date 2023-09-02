using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Ids;
using Shared.Server.Dto;

namespace Shared.Server.Services;

[Service]
public interface ITestGrpcService
{
    Task<TestsDto> GetTests(TaskId taskId);
}