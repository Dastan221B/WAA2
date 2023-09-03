using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExcludeNullValuesExtension
{

    public static List<T> ExcludeNull<T>(this IEnumerable<T> source)
    {
        List<T> array = new List<T>();
        foreach (var item in source)
        {
            if (item != null)
                array.Add(item);
        }
        return array;
    }
}