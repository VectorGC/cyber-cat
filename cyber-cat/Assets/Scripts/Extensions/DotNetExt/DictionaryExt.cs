using System.Collections.Generic;

namespace Extensions.DotNetExt
{
    public static class DictionaryExt
    {
        public static TValue GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            var success = dictionary.TryGetValue(key, out var value);
            if (success)
            {
                return value;
            }

            throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
        }
    }
}