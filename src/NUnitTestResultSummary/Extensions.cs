using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestResultSummary
{
    public static class Extensions
    {
        public static bool In(this string @this, params string[] items)
        {
            if (string.IsNullOrEmpty(@this))
            {
                return false;
            }
            
            foreach (var item in items)
            {
                if (item.Equals(@this, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
