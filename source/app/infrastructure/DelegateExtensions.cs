using System;

namespace app.infrastructure
{
    public static class DelegateExtensions
    {
        public static Func<ResultToCache> memoize<ResultToCache>(this Func<ResultToCache> original)
        {
            var is_cached = false;
            ResultToCache cached_value = default(ResultToCache);

            return () =>
            {
                if (is_cached) return cached_value;

                is_cached = true;
                return (cached_value =original());
            };
        }
    }
}