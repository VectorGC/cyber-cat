using ApiGateway.Client.Models;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;

namespace ApiGateway.Client.Internal.Tasks.Verdicts
{
    public class Failure : IVerdict
    {
        public int TestsPassed { get; }
        public string Error { get; }

        public Failure(VerdictData verdictData)
        {
            TestsPassed = verdictData.TestsPassed;
            Error = verdictData.Error;
        }
    }
}