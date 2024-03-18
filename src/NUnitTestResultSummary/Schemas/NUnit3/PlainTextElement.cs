using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    public class PlainTextElement
    {
        [XmlText]
        public string Value { get; set; }
    }
}
