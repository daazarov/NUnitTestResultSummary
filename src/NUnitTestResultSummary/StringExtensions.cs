using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestResultSummary
{
    public static class StringExtensions
    {
        public static string MarkdownCodeBlock(this string @this)
        {
            return $"```{@this}```";
        }

        public static string MarkdownSubscript(this string @this)
        {
            return $"<sub>{@this}</sub>";
        }
    }
}
