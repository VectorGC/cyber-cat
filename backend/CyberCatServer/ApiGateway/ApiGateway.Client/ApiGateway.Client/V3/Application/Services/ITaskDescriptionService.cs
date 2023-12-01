using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Domain.Tasks;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Application.Services
{
    public interface ITaskDescriptionService
    {
        Task<List<TaskDescription>> GetTaskDescriptions(AuthorizationToken token);
    }
}