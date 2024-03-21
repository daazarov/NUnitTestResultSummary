using System.Collections;
using System.Text;

namespace NUnitTestResultSummary
{
    public static class StringBuilderExtensions
    {
        private const string CollapsedSectionTemplate = "<details>\n<summary>{0}</summary>\n{1}\n</details> ";
        private const string InnerCollapsedSectionTemplate = "<details><summary>{0}</summary>{1}</details> ";

        public static StringBuilder MarkdownCollapsedSection(this StringBuilder stringBuilder, string summary, string details)
        {
            Guard(stringBuilder);
            Guard(summary);
            Guard(details);

            return stringBuilder.AppendLine(string.Format(CollapsedSectionTemplate, summary, details));
        }

        public static string MarkdownInnerCollapsedSection(this StringBuilder stringBuilder, string summary, string details)
        {
            Guard(stringBuilder);
            Guard(summary);
            Guard(details);

            return new StringBuilder().Append(string.Format(InnerCollapsedSectionTemplate, summary, details)).ToString();
        }

        public static StringBuilder MarkdownBadge(this StringBuilder stringBuilder, string linkText, string link)
        {
            Guard(stringBuilder);
            Guard(linkText);
            Guard(link);

            return stringBuilder.AppendLine($"![{linkText}]({link})");
        }

        public static StringBuilder MarkdownTableHeader(this StringBuilder stringBuilder, params string[] columnNames)
        {
            Guard(stringBuilder);
            Guard(columnNames);

            stringBuilder.AppendLine().Append($"|");

            foreach (var columnName in columnNames)
            {
                stringBuilder.Append($" {columnName} |");
            }

            stringBuilder.AppendLine().Append($"|");

            foreach (var columnName in columnNames)
            {
                stringBuilder.Append(" --- | ");
            }

            stringBuilder.AppendLine();

            return stringBuilder;
        }

        public static StringBuilder MarkdownTableRow(this StringBuilder stringBuilder, params string[] rows)
        {
            Guard(stringBuilder);
            Guard(rows);

            stringBuilder.Append($"|");

            foreach (var row in rows)
            {
                stringBuilder.Append($" {row} |");
            }

            stringBuilder.AppendLine();

            return stringBuilder;
        }

        private static void Guard(object argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(nameof(argument));
            }
            else if (argument is string str)
            {
                if (string.IsNullOrEmpty(str))
                    throw new ArgumentNullException(nameof(argument));
            }
            else if (argument is IEnumerable col)
            { 
                if(col == null || !col.Cast<object>().Any())
                    throw new ArgumentNullException(nameof(argument));
            }
        }
    }
}
