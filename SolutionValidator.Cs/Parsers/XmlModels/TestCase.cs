using System.Xml.Serialization;

namespace SolutionValidator.Cs.Parsers.XmlModels
{
#nullable disable
    public class TestCase
    {
        [XmlAttribute(AttributeName = TestCaseElementNames.Name)]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = TestCaseElementNames.Id)]
        public string Id { get; set; }

        [XmlElement(ElementName = FailureElementNames.Root)]
        public Failure Failure { get; set; }

        [XmlAttribute(AttributeName = TestCaseElementNames.Result)]
        public ResultType Result { get; set; }

        [XmlAttribute(AttributeName = TestCaseElementNames.StartTime)]
        public DateTime StartTime { get; set; }

        [XmlAttribute(AttributeName = TestCaseElementNames.EndTime)]
        public DateTime EndTime { get; set; }

        [XmlAttribute(AttributeName = TestCaseElementNames.DurationSeconds)]
        public double DurationSeconds { get; set; }

        [XmlAttribute(AttributeName = TestCaseElementNames.ClassName)]
        public string ClassName { get; set; }

        [XmlAttribute(AttributeName = TestCaseElementNames.MethodName)]
        public string MethodName { get; set; }
    }
#nullable enable
}
