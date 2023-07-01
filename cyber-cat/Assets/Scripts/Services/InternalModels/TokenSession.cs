namespace Services.InternalModels
{
    internal class TokenSession : ITokenSession
    {
        public string Value { get; }

        public TokenSession(string value)
        {
            Value = value;
        }
    }
}