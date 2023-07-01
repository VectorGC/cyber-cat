using System;
using System.Collections;
using System.Collections.Generic;

namespace RestAPIWrapper
{
    public class WebErrorLocalize : IReadOnlyDictionary<WebError, string>
    {
        private readonly IReadOnlyDictionary<WebError, string> _dictionary = new Dictionary<WebError, string>()
        {
            {WebError.NoError, "Ошибок нет"},
            {WebError.Internal, "Внутренняя ошибка сервера"},

            {WebError.EmailNotProvided, "Email не был введен"},
            {WebError.EmailInvalid, "Email был введен с ошибкой"},
            {WebError.EmailUnknown, "Email не зарегистрирован"},
            {WebError.EmailTaken, "Такой email уже зарегистрирован"},
            {WebError.TokenNotVerified, "Вход с неизвестного IP, Вам на почту пришло сообщение с подтверждением"},

            {WebError.PasswordNotProvided, "Пароль не был введен"},
            {WebError.PasswordInvalid, "Пароль должен содержать хотя бы 6 символов"},
            {WebError.PasswordWrong, "Пароль неправильный"},

            {WebError.LanguageNotSupported, "Данный язык не может быть обработан"},

            {WebError.NameInvalid, "Имя должно содержать менее 128 символов"},

            {WebError.SolutionTimeoutFail, "Время выполнение программы превышено"},
            {WebError.SolutionRuntimeFail, "Во время выполнения возникла ошибка"}
        };

        public string this[int errorKey] => this[(WebError) errorKey];

        private static WebErrorLocalize _instance = new WebErrorLocalize();

        public static string Localize(string errorCode)
        {
            var success = Enum.TryParse<WebError>(errorCode, out var webError);
            if (!success)
            {
                return $"Неизвестный код ошибки '{errorCode}'";
            }

            return Localize(webError);
        }
        
        public static string Localize(WebError errorCode)
        {
            return _instance[errorCode];
        }

        private string GetErrorLocalize(WebError webError)
        {
            var success = _dictionary.TryGetValue(webError, out var localized);
            if (!success)
            {
                return $"Внутренняя ошибка ({(int) webError})";
            }

            return $"{localized} ({(int) webError})";
        }

        #region | Delegate implementation |

        public IEnumerator<KeyValuePair<WebError, string>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _dictionary).GetEnumerator();
        }

        public int Count => _dictionary.Count;

        public bool ContainsKey(WebError key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(WebError key, out string value)
        {
            value = GetErrorLocalize(key);
            return true;
        }

        public string this[WebError key] => GetErrorLocalize(key);

        public IEnumerable<WebError> Keys => _dictionary.Keys;

        public IEnumerable<string> Values => _dictionary.Values;

        #endregion | Delegate implementation |
    }
}