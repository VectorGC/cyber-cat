using System.Collections.Specialized;
using System.Web;
using UnityEngine;

namespace Authentication
{
    public class AuthenticationData
    {
        public string Login { get; }
        public string Password { get; }

        public AuthenticationData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public NameValueCollection AsQueryParams()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query.Add("email", Login);
            query.Add("pass", Password);

            return query;
        }
    }
}