using System.Diagnostics;
using System.Threading.Tasks;

namespace ApiGateway.Client.V2
{
    public class Dev : IAccessV2
    {
        public bool IsAvailable { get; private set; }

        private readonly WebClient _webClient;

        public Dev(WebClient webClient)
        {
            _webClient = webClient;
            SetDebug();
        }

        public async Task RemoveUser(string email)
        {
            await _webClient.RemoveUser(email);
        }

        [Conditional("DEBUG")]
        private void SetDebug()
        {
            IsAvailable = true;
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}