using System.Globalization;
using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    [XmlRoot(ElementName = "test-run")]
    public class NUnit3XmlTestsResult
    {
        #region Attributes

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public string FullName { get; set; }

        [XmlAttribute(AttributeName = "runstate")]
        public string Runstate { get; set; }

        [XmlAttribute(AttributeName = "testcasecount")]
        public int TestcaseCount { get; set; }

        [XmlAttribute(AttributeName = "result")]
        public string ResultString { get; set; }

        public Result Result => (Result)Enum.Parse(typeof(Result), ResultString);

        [XmlAttribute(AttributeName = "total")]
        public int Total { get; set; }

        [XmlAttribute(AttributeName = "passed")]
        public int Passed { get; set; }

        [XmlAttribute(AttributeName = "failed")]
        public int Failed { get; set; }

        [XmlAttribute(AttributeName = "skipped")]
        public int Skipped { get; set; }

        [XmlAttribute(AttributeName = "inconclusive")]
        public int Inconclusive { get; set; }

        [XmlAttribute(AttributeName = "warnings")]
        public int Warnings { get; set; }

        [XmlAttribute(AttributeName = "asserts")]
        public int Asserts { get; set; }

        [XmlAttribute(AttributeName = "engine-version")]
        public string EngineVersion { get; set; }

        [XmlAttribute(AttributeName = "clr-version")]
        public string ClrVersion { get; set; }

        [XmlAttribute(AttributeName = "start-time")]
        public string StartTime { get; set; }

        [XmlAttribute(AttributeName = "end-time")]
        public string EndTime { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public string DurationString { get; set; }

        public TimeSpan Duration => TimeSpan.ParseExact(DurationString, @"s\.ffffff", CultureInfo.InvariantCulture);

        #endregion

        [XmlElement(ElementName = "command-line")]
        public CommandLineElement CommandLine { get; set; }

        [XmlElement(ElementName = "test-suite")]
        public TestSuiteElement TestSuite { get; set; }
    }
}
