using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.ApprenticeFeedback.Domain.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool ContainsCountItems<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (null == source) throw new ArgumentException("Enumeration cannot be null", nameof(source));
            if (count < 0) throw new ArgumentException("Count must be greater than or equal to zero", nameof(count));
            return source.Take(count + 1).Count() == count;
        }
    }
}
