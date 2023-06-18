namespace ServerAPI
{
    public enum ServerEnvironment
    {
        Serverless = 0,
        LocalServer,
        DockerLocalServer,
        Production,
        Production_Http,
    }
}