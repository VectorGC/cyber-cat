namespace ApiGateway;

public class ApiGatewayAppSettings
{
    public class ConnectionStringsSettings
    {
        public string AuthServiceGrpcEndpoint { get; set; }
        public string TaskServiceGrpcEndpoint { get; set; }
        public string SolutionServiceGrpcEndpoint { get; set; }
        public string JudgeServiceGrpcEndpoint { get; set; }
        public string PlayerServiceGrpcEndpoint { get; set; }
        

        public Uri AuthServiceGrpcAddress => new(AuthServiceGrpcEndpoint);
        public Uri TaskServiceGrpcAddress => new(TaskServiceGrpcEndpoint);
        public Uri SolutionServiceGrpcAddress => new(SolutionServiceGrpcEndpoint);
        public Uri JudgeServiceGrpcAddress => new(JudgeServiceGrpcEndpoint);
        public Uri PlayerServiceGrpcAddress => new(PlayerServiceGrpcEndpoint);
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}