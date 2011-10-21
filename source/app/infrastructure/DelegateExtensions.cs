using System;

namespace app.infrastructure
{
    public static class DelegateExtensions
    {
        public static Func<ResultToCache> memoize<ResultToCache>(this Func<ResultToCache> original)
        {
            return original;
        }
    }
}