﻿using System.Xml.Serialization;

namespace NUnitTestResultSummary.Schemas.NUnit3
{
    public class CommandLineElement
    {
        [XmlText]
        public string Value { get; set; }
    }
}
