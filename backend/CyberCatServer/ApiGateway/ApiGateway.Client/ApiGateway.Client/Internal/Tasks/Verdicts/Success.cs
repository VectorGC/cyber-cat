using ApiGateway.Client.Models;
using Shared.Models.Data;

namespace ApiGateway.Client.Internal.Tasks.Verdicts
{
    public class Success : IVerdict
    {
        public int TestsPassed { get; }

        public Success(VerdictData verdictData)
        {
            TestsPassed = verdictData.TestsPassed;
        }

        public override string ToString()
        {
            return $"Success: {TestsPassed} test passed";
        }
    }
}