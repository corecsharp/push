using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Extentions.QueryExtention
{
    public static class QueryExtention
    {
        public static void Foreach<T>(this IEnumerable<T> items, Action<T> act)
        {
            foreach (var item in items)
                act(item);
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> items, bool condition, Func<T, bool> predicate)
        {
            return condition ? items.Where(predicate) : items;
        }
    }
}
