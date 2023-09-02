using ApiGateway.Client.Models;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;

namespace ApiGateway.Client.Internal.Tasks.Verdicts
{
    public class Success : IVerdict
    {
        public int TestsPassed { get; }

        public Success(VerdictData verdictData)
        {
            TestsPassed = verdictData.TestsPassed;
        }
    }
}