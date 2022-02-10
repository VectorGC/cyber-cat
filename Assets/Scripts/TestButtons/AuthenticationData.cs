namespace DefaultNamespace.TestButtons
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

        public string GetFormattedQuery(string templateQuery) => string.Format(templateQuery, Login, Password);
    }
}