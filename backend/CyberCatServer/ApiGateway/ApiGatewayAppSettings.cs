using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ApiGateway;

public class ApiGatewayAppSettings
{
    public class ConnectionStringsSettings
    {
        public string MongoDatabase { get; set; }
        public string AuthServiceGrpcEndpoint { get; set; }
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}