using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqExercises.Extensions
{
    public static class GenericExtensions
    {
        public static bool In<T>(this T value, params T[] list)
        {
            return list.Contains(value);
        }
    }
}