using System.Xml.Serialization;

namespace SolutionValidator.Cs.Parsers.XmlModels
{
#nullable disable
    [XmlRoot(ElementName = TestSuiteElementNames.Root)]
    public class TestSuite
    {
        [XmlElement(ElementName = FailureElementNames.Root)]
        public Failure Failure { get; set; }

        [XmlAttribute(AttributeName = TestSuiteElementNames.Id)]
        public string Id { get; set; }

        [XmlElement(ElementName = TestSuiteElementNames.Root)]
        public TestSuite NestedTestSuite { get; set; }

        [XmlElement(ElementName = TestCaseElementNames.Root)]
        public List<TestCase> TestCases { get; set; }
    }
#nullable enable
}
