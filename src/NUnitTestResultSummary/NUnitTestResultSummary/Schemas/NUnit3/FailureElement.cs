using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    public class FailureElement
    {
        [XmlElement(ElementName = "message")]
        public PlainTextElement Message { get; set; }
    }
}
