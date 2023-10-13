using System;

namespace ApiGateway.Client
{
    public enum ServerEnvironment
    {
        Serverless = 0,
        Localhost,
        Production
    }

    internal static class ServerEnvironmentMap
    {
        public static Uri Get(ServerEnvironment environment)
        {
            switch (environment)
            {
                case ServerEnvironment.Localhost:
                    // Send to Api Gateway local instance directly.
                    return new Uri("http://localhost");
                case ServerEnvironment.Production:
                    return new Uri("https://server.cyber-cat.pro");
                default:
                    throw new ArgumentOutOfRangeException(nameof(ServerEnvironment));
            }
        }

        public static Uri GetUri(this ServerEnvironment environment)
        {
            switch (environment)
            {
                case ServerEnvironment.Localhost:
                    // Send to Api Gateway local instance directly.
                    return new Uri("http://localhost");
                case ServerEnvironment.Production:
                    return new Uri("https://server.cyber-cat.pro");
                default:
                    throw new ArgumentOutOfRangeException(nameof(ServerEnvironment));
            }
        }
    }
}