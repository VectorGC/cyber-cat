using TMPro;

namespace Authentication
{
    public static class StringExt
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        
        public static bool IsNullOrEmpty(this TMP_InputField value)
        {
            return string.IsNullOrEmpty(value.text);
        }
        
        
    }
}