using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T RandomItem<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static int Average(this IList<int> list)
    {
        if (list.Count == 0) return 0;

        int sum = 0;
        foreach (int i in list)
        {
            sum += i;
        }
        return Mathf.FloorToInt(sum / list.Count);
    }

    public static int Plus1Wrap0(this int val, int max)
    {
        return (val + 1) % max;
    }
}