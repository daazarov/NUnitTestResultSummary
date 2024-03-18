using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    public class ReasonElement
    {
        [XmlElement(ElementName = "message")]
        public PlainTextElement Message { get; set; }
    }
}
