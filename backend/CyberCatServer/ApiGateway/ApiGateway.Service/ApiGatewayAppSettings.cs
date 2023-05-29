namespace ApiGateway.Service;

public class ApiGatewayAppSettings
{
    public class ConnectionStringsSettings
    {
        public string AuthServiceGrpcEndpoint { get; set; }
        public string TaskServiceGrpcEndpoint { get; set; }
        public string SolutionServiceGrpcEndpoint { get; set; }
        public string JudgeServiceGrpcEndpoint { get; set; }

        public Uri AuthServiceGrpcAddress => new(AuthServiceGrpcEndpoint);
        public Uri TaskServiceGrpcAddress => new(TaskServiceGrpcEndpoint);
        public Uri SolutionServiceGrpcAddress => new(SolutionServiceGrpcEndpoint);
        public Uri JudgeServiceGrpcAddress => new(JudgeServiceGrpcEndpoint);
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}