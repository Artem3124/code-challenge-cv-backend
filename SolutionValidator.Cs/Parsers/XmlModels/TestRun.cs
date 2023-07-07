using System.Xml.Serialization;

namespace SolutionValidator.Cs.Parsers.XmlModels
{
#nullable disable
    [XmlRoot(ElementName = TestRunElementNames.Root)]
    public class TestRun
    {
        [XmlElement(ElementName = TestSuiteElementNames.Root)]
        public TestSuite TestSuite { get; set; }

        [XmlAttribute(AttributeName = "result")]
        public ResultType Result { get; set; }
    }
#nullable enable
}
