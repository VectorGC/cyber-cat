using UnityEngine;

namespace RestAPIWrapper.Server
{
    public static class Endpoint
    {
        private static string _root;
        private static string ROOT
        {
            get
            {
                if (string.IsNullOrEmpty(_root))
                {
                    RefreshRoot();
                }
                return _root;
            }
        }

        internal static string TASKS_HIERARCHY = ROOT + "/tasks/hierarchy";
        internal static string TASKS_FLAT = ROOT + "/tasks/flat";
        internal static string SOLUTION = ROOT + "/solution";

        internal static string LOGIN = ROOT + "/login";
        internal static string REGISTER = ROOT + "/register";
        internal static string RESTORE = ROOT + "/restore";

        private static async void RefreshRoot()
        {
            var baseServer = Resources.Load<BaseServer>("Base server");
            _root = await baseServer.GetActualServerURL();
        }
    }
}