using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    public class PropertyElement
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
