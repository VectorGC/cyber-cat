namespace RestAPIWrapper.Server
{
    public static class Endpoint
    {
        private const string ROOT = "https://cyber-cat.kee-reel.com";

        internal const string TASKS_HIERARCHY = ROOT + "/tasks/hierarchy";
        internal const string TASKS_FLAT = ROOT + "/tasks/flat";
        internal const string SOLUTION = ROOT + "/solution";

        internal const string LOGIN = ROOT + "/login";
        internal const string REGISTER = ROOT + "/register";
        internal const string RESTORE = ROOT + "/restore";
    }
}