using System;

namespace ApiGateway.Client.V2
{
    public class Credentials : IAccess
    {
        public event Action<string> TokenChanged;

        public string Email { get; set; }
        public string Name { get; set; }

        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                TokenChanged?.Invoke(_token);
            }
        }

        public bool IsAvailable => true;

        private string _token;

        public void Dispose()
        {
        }
    }
}