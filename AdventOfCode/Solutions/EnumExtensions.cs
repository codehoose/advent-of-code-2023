using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    internal static class EnumExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
