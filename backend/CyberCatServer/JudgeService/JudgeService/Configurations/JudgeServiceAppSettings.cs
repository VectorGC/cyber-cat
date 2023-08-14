namespace JudgeService.Configurations;

public class JudgeServiceAppSettings
{
    public class ConnectionStringsSettings
    {
        public string CppLauncherServiceGrpcEndpoint { get; set; }
        public string TestServiceGrpcEndpoint { get; set; }

        public Uri CppLauncherServiceGrpcAddress => new(CppLauncherServiceGrpcEndpoint);
        public Uri TestServiceGrpcAddress => new(TestServiceGrpcEndpoint);
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}