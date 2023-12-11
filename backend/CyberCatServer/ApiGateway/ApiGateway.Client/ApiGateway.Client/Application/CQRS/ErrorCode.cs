using System;
using System.Collections.Generic;

namespace ApiGateway.Client.Application.CQRS
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

    public class ErrorCodeException : Exception
    {
        public ErrorCode Code { get; }

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

        public ErrorCodeException(ErrorCode code) : this(code, _messages[code])
        {
        }

        public ErrorCodeException(ErrorCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}