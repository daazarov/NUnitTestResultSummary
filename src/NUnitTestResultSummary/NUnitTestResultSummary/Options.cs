using CommandLine;

namespace NUnitTestResultSummary
{
    public class Options
    {
        [Option(longName: "input", Required = true, HelpText = "File path to analyse.")]
        public string InputFile { get; set; }

        [Option(longName: "badge", Required = false, HelpText = "Include a number of tests taken badge in the output using shields.io - true (default) or false.", Default = "true")]
        public string BadgeString { get; set; }

        public bool Badge => BadgeString.Equals("true", StringComparison.OrdinalIgnoreCase);

        [Option(longName: "format", Required = false, HelpText = "Output Format - (only markdown available right now.)", Default = "markdown")]
        public string OutputFormatString { get; set; }

        public OutputFormat OutputFormat => Enum.TryParse(OutputFormatString.ToLower(), out OutputFormat format) ? format : throw new ArgumentOutOfRangeException(nameof(OutputFormatString));

        [Option(longName: "output", Required = false, HelpText = "Output Type - (only markdown file (*.md) available right now.)", Default = "file")]
        public string Output { get; set; }

        [Option(longName: "skipped", Required = false, HelpText = "Whether skipped tests should be included in the report - true (default) or false.", Default = "true")]
        public string ShowSkippedString { get; set; }

        public bool ShowSkipped => ShowSkippedString.Equals("true", StringComparison.OrdinalIgnoreCase);

        [Option(longName: "inconclusive", Required = false, HelpText = "Whether inconclusive tests should be included in the report - true (default) or false.", Default = "true")]
        public string ShowInconclusiveString {  get; set; }

        public bool ShowInconclusive => ShowInconclusiveString.Equals("true", StringComparison.OrdinalIgnoreCase);

        [Option(longName: "passed", Required = false, HelpText = "Whether passed tests should be included in the report - true or false (default).", Default = "false")]
        public string ShowPassedString { get; set; }

        public bool ShowPassed => ShowInconclusiveString.Equals("true", StringComparison.OrdinalIgnoreCase);
    }
}
