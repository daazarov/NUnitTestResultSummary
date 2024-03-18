using System.Globalization;
using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    public class TestCaseElement
    {
        #region Attributes

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public string FullName { get; set; }

        [XmlAttribute(AttributeName = "methodname")]
        public string MethodName { get; set; }

        [XmlAttribute(AttributeName = "classname")]
        public string ClassName { get; set; }

        [XmlAttribute(AttributeName = "runstate")]
        public string Runstate { get; set; }

        [XmlAttribute(AttributeName = "seed")]
        public long Seed { get; set; }

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

        [XmlAttribute(AttributeName = "asserts")]
        public int Asserts { get; set; }

        #endregion

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public SettingElement[] Properties { get; set; }

        [XmlElement(ElementName = "reason")]
        public ReasonElement Reason { get; set; }

        [XmlElement(ElementName = "failure")]
        public FailureElement Failure { get; set; }

        [XmlElement(ElementName = "output")]
        public PlainTextElement Output { get; set; }
    }
}
