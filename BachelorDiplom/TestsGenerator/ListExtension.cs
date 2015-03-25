using System;
using System.Collections.Generic;

namespace BachelorLibAPI.TestsGenerator
{
    public static class ListExtension
    {
        public static T Random<T>(this IList<T> list, Random random)
        {
            return list[random.Next(0, list.Count)];
        }
    }
}

