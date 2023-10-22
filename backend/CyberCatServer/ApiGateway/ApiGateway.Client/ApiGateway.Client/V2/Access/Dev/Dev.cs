using System.Diagnostics;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.V2.Access.Dev
{
    public class Dev : IAccess
    {
        public bool IsAvailable { get; private set; }
        public UsersDev Users { get; }

        internal Dev(WebClient webClient)
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