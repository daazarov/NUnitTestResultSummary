using System.Globalization;
using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    public class TestSuiteElement
    {
        #region Attributes

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

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

        [XmlAttribute(AttributeName = "site")]
        public string Site { get; set; }

        [XmlAttribute(AttributeName = "label")]
        public string Label { get; set; }

        [XmlAttribute(AttributeName = "start-time")]
        public string StartTime { get; set; }

        [XmlAttribute(AttributeName = "end-time")]
        public string EndTime { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public string DurationString { get; set; }

        public TimeSpan Duration => TimeSpan.ParseExact(DurationString, @"s\.ffffff", CultureInfo.InvariantCulture);

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

        #endregion

        [XmlElement(ElementName = "test-suite")]
        public TestSuiteElement[] Inners { get; set; }

        [XmlArray("settings")]
        [XmlArrayItem("setting")]
        public SettingElement[] Settings { get; set; }

        [XmlElement(ElementName = "environment")]
        public EnvironmenElement Environment { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public SettingElement[] Properties { get; set; }

        [XmlElement(ElementName = "reason")]
        public ReasonElement Reason { get; set; }

        [XmlElement(ElementName = "failure")]
        public FailureElement Failure { get; set; }

        [XmlElement(ElementName = "output")]
        public PlainTextElement Output { get; set; }

        [XmlElement(ElementName = "test-case")]
        public TestCaseElement[] TestCases { get; set; }
    }
}
