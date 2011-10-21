using System;

namespace app.infrastructure
{
    public static class DelegateExtensions
    {
        public static Func<ItemToCache> memoize<ItemToCache>(this Func<ItemToCache> original)
        {
            return original;
        }
    }
}