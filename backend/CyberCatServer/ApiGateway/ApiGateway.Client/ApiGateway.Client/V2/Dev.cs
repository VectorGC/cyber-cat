using System.Diagnostics;
using System.Threading.Tasks;

namespace ApiGateway.Client.V2
{
    public class Dev : IAccess
    {
        public bool IsAvailable { get; private set; }

        private readonly WebClientAccess _webClientAccess;

        public Dev(WebClientAccess webClientAccess)
        {
            _webClientAccess = webClientAccess;
            SetDebug();
        }

        public async Task RemoveUser(string email)
        {
            await _webClientAccess.RemoveUser(email);
        }

        [Conditional("DEBUG")]
        private void SetDebug()
        {
            IsAvailable = true;
        }

        public void Dispose()
        {
            _webClientAccess.Dispose();
        }
    }
}