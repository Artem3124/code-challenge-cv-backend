using System.Xml.Serialization;

namespace SolutionValidator.Cs.Parsers.XmlModels
{
#nullable disable
    public class Failure
    {
        [XmlElement(ElementName = FailureElementNames.Message)]
        public string Message { get; set; }
    }
#nullable enable
}
