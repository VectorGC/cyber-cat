namespace Tests.InternalMockModels
{
    internal class MockToken : ITokenSession
    {
        public string Value { get; }

        public MockToken(string value)
        {
            Value = value;
        }
    }
}