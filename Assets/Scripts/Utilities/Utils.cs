using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static T ElementAt<T>(this IEnumerable<T> collection, int index)
    {
        if (index < 0)
        {
            return default;
        }
        int i = 0;
        T t = default;
        foreach (var item in collection)
        {
            if (i == index)
            {
                t = item;
            }
            i++;
        }
        return t;
    }
}
