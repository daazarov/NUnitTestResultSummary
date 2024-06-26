﻿using System.Text;
using System.Text.RegularExpressions;

using NUnitTestResultSummary.Schemas.NUnit3;

namespace NUnitTestResultSummary
{
    public class MarkdownOutputGenerator : IOutputGenerator
    {
        private readonly StringBuilder _markdownOutput = new StringBuilder();

        public string GenerateOutput(ResultSummary summary, Options options)
        {
            if (options.Badge)
            {
                _markdownOutput.AppendLine()
                               .MarkdownBadge("Total Tests", BuildShieldsIoUrl("Total Tests", summary.TotalTestCount.ToString(), "white"))
                               .MarkdownBadge("Passed Tests", BuildShieldsIoUrl("Passed Tests", summary.PassedTestCount.ToString(), "green"))
                               .MarkdownBadge("Failed Tests", BuildShieldsIoUrl("Failed Tests", summary.FailedTestCount.ToString(), "red"))
                               .MarkdownBadge("Skipped Tests", BuildShieldsIoUrl("Skipped Tests", summary.SkippedTestCount.ToString(), "blue"))
                               .MarkdownBadge("Warning Tests", BuildShieldsIoUrl("Warning Tests", summary.WarningTestCount.ToString(), "orange"))
                               .MarkdownBadge("Inconclusive Tests", BuildShieldsIoUrl("Inconclusive Tests", summary.InconclusiveTestCount.ToString(), "white"))
                               .AppendLine();
            }

            AddSection((output) => output.MarkdownCollapsedSection("Failed Tests", MarkdownTable(() => summary.Failed)), () => summary.FailedTestCount > 0);
            AddSection((output) => output.MarkdownCollapsedSection("Warning Tests", MarkdownTable(() => summary.Warning)), () => summary.WarningTestCount > 0);
            AddSection((output) => output.MarkdownCollapsedSection("Skipped Tests", MarkdownTable(() => summary.Skipped)), () => options.ShowSkipped && summary.SkippedTestCount > 0);
            AddSection((output) => output.MarkdownCollapsedSection("Inconclusive Tests", MarkdownTable(() => summary.Inconclusive)), () => options.ShowInconclusive && summary.InconclusiveTestCount > 0);
            AddSection((output) => output.MarkdownCollapsedSection("Passed Tests", MarkdownTable(() => summary.Passed)), () => options.ShowPassed && summary.PassedTestCount > 0);

            return _markdownOutput.ToString();
        }

        private void AddSection(Action<StringBuilder> action, Func<bool> predicate = null)
        {
            if (predicate != null)
            {
                if(predicate()) action(_markdownOutput);
            }
            else
            {
                action(_markdownOutput);
            }
        }

        private string MarkdownTable(Func<TestCaseElement[]> accessor)
        {
            var cases = accessor.Invoke();
            var builder = new StringBuilder();

            if (cases.Any(x => x.Reason != null || x.Failure != null))
            {
                builder.MarkdownTableHeader("Name", "Result", "Reason");
            }
            else
            { 
                builder.MarkdownTableHeader("Name", "Result", "Duration");
            }

            foreach (var item in cases)
            {
                if (item.Reason != null)
                {
                    builder.MarkdownTableRow
                    (
                        item.MethodName.MarkdownCodeBlock(),
                        GenerateResultIndicator(item.Result),
                        builder.MarkdownInnerCollapsedSection("Details", item.Reason.Message.Value.ReplaceLineEndings("<br>").MarkdownSubscript())
                    );
                }
                else if (item.Failure != null)
                {
                    builder.MarkdownTableRow
                    (
                        item.MethodName.MarkdownCodeBlock(),
                        GenerateResultIndicator(item.Result),
                        builder.MarkdownInnerCollapsedSection("Details", item.Failure.Message.Value.ReplaceLineEndings("<br>").MarkdownSubscript())
                    );
                }
                else
                {
                    builder.MarkdownTableRow
                    (
                        item.MethodName.MarkdownCodeBlock(),
                        GenerateResultIndicator(item.Result),
                        item.Duration.ToString()
                    );
                }
            }

            builder.AppendLine();

            return builder.ToString();
        }

        private string GenerateResultIndicator(Result result)
        {
            switch (result)
            {
                case Result.Passed:
                    return $"{ result.ToString() } :green_circle:";
                case Result.Failed:
                    return $"{ result.ToString() } :red_circle:";
                case Result.Warning:
                    return $"{ result.ToString() } :orange_circle:";
                case Result.Inconclusive:
                case Result.Skipped:
                    return $"{ result.ToString() } :large_blue_circle:";
                default:
                    return result.ToString();
            }
        }

        private string BuildShieldsIoUrl(string label, string message, string color)
        {
            return $"https://img.shields.io/badge/{label.Replace(' ', '_')}-{message.Replace(' ', '_')}-{color}";
        }
    }
}
