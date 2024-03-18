using CommandLine;
using NUnitTestResultSummary.Schemas.NUnit3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NUnitTestResultSummary
{
    public class NUnitParser
    {
        private static readonly Lazy<NUnitParser<NUnit3XmlTestsResult>> V3DefaultParser = new Lazy<NUnitParser<NUnit3XmlTestsResult>>();

        public static NUnitParser<NUnit3XmlTestsResult> NUnit3
        {
            get { return V3DefaultParser.Value; }
        }
    }

    public class NUnitParser<T>
    {
        public int ParseXmlResult(string xmlInput, Func<T, int> handleResult, Func<Exception, int> handleError = null)
        {
            var serializer = new XmlSerializer(typeof(T));

            try
            {
                using (var reader = new StringReader(xmlInput))
                {
                    var nUnitSummary = (T)serializer.Deserialize(reader);

                    return handleResult(nUnitSummary);
                }
            }
            catch (Exception e)
            {
                if (handleError != null)
                {
                    return handleError(e);
                }

                return -2;
            }
            
        }
    }
}
