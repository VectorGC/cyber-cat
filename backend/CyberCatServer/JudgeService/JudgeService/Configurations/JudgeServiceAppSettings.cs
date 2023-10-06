namespace JudgeService.Configurations;

public class JudgeServiceAppSettings
{
    public class ConnectionStringsSettings
    {
        public string CppLauncherServiceGrpcEndpoint { get; set; }
        public string TaskServiceGrpcEndpoint { get; set; }

        public Uri CppLauncherServiceGrpcAddress => new(CppLauncherServiceGrpcEndpoint);
        public Uri TaskServiceGrpcAddress => new(TaskServiceGrpcEndpoint);
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}