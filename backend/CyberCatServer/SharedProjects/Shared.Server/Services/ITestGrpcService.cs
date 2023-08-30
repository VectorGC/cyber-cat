using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace Shared.Server.Services;

[Service]
public interface ITestGrpcService
{
    Task<TestsDto> GetTests(TaskId taskId);
}