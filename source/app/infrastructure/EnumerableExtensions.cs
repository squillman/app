using System;
using System.Collections.Generic;

namespace app.infrastructure
{
    public static class EnumerableExtensions
    {
        public static void each<T>(this IEnumerable<T> all_items, Action<T> visitor )
        {
            foreach (var item in all_items) visitor(item);
        }
    }
}