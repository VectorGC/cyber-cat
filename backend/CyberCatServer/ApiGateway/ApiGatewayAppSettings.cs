namespace ApiGateway;

public class ApiGatewayAppSettings
{
    public class ConnectionStringsSettings
    {
        public string AuthServiceGrpcEndpoint { get; set; }
        public string TaskServiceGrpcEndpoint { get; set; }
        public string SolutionServiceGrpcEndpoint { get; set; }

        public Uri AuthServiceGrpcAddress => new(AuthServiceGrpcEndpoint);
        public Uri TaskServiceGrpcAddress => new(TaskServiceGrpcEndpoint);
        public Uri SolutionServiceGrpcAddress => new(SolutionServiceGrpcEndpoint);
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}