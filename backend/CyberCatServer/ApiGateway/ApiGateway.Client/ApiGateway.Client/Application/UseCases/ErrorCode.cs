using System.Collections.Generic;

namespace ApiGateway.Client.Application.UseCases
{
    public enum ErrorCode
    {
        None = 0,
        Unknown = 1,
        AlreadyLoggined,
        NotLoggined,
        Exception,
        NotAvailableTaskForAnonymous,
        EmailEmpty,
        PasswordEmpty,
        UserNameEmpty
    }

    public static class ErrorMessages
    {
        private static readonly Dictionary<ErrorCode, string> _messages = new Dictionary<ErrorCode, string>()
        {
            [ErrorCode.None] = string.Empty,
            [ErrorCode.Unknown] = "Неизвестная ошибка. Обратитесь к администратору",
            [ErrorCode.AlreadyLoggined] = "Сперва нужно выйти из текущей учетной записи",
            [ErrorCode.NotLoggined] = "Требуется войти в учетную запись",
            [ErrorCode.NotAvailableTaskForAnonymous] = "Для решения задачи требуется войти или зарегистрироваться",
            [ErrorCode.EmailEmpty] = "Email не может быть пустым",
            [ErrorCode.PasswordEmpty] = "Пароль не может быть пустым",
            [ErrorCode.UserNameEmpty] = "Имя пользователя не может быть пустым",
        };

        public static string Message(this ErrorCode errorCode)
        {
            return _messages[errorCode];
        }
    }
}