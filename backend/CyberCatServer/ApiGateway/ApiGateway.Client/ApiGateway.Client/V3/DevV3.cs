using System.Diagnostics;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.V3
{
    public class DevV3 : IAccessV3
    {
        public bool IsAvailable { get; private set; }
        public UsersDev Users { get; }

        internal DevV3(WebClient webClient)
        {
            Users = new UsersDev(webClient);
            SetDebug();
        }

        [Conditional("DEBUG")]
        private void SetDebug()
        {
            IsAvailable = true;
        }
    }
}