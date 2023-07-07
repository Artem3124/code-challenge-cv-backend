using CodeProblemAssistant.Data.Enums;

namespace CodeProblemAssistant.Contract.Models
{
#nullable disable
    public class CodeProblemCreateRequest
    {
        public string Name { get; set; }

        public CodeProblemComplexity ComplexityTypeId { get; set; }

        public string Description { get; set; }

        public List<string> Constraints { get; set; }

        public List<Example> Examples { get; set; }

        public List<string> Tags { get; set; }

        public List<string> ParameterNames { get; set; }

        public List<InternalType> ParameterTypes { get; set; }

        public string Explanation { get; set; }

        public InternalType ReturnType { get; set; }

        public Guid TestCaseSetUUID { get; set; }
    }
#nullable enable
}
