using CodeProblemAssistant.Data.Enums;
using Shared.Core.Models;

namespace CodeProblemAssistant.Data.Models
{
    public class CodeProblem : Entity
    {
        public string Name { get; set; }

        public CodeProblemComplexity ComplexityTypeId { get; set; }

        public string Description { get; set; }

        public string ConstraintsJson { get; set; }

        public string ExamplesJson { get; set; }

        public List<Tag> Tags { get; set; } = new();

        public Guid TestCaseSetUUID { get; set; }

        public string ParameterNamesCsv { get; set; }

        public string ParameterTypesCsv { get; set; }

        public InternalType ReturnType { get; set; }

        public string Explanation { get; set; }

        public List<Comment> Comments { get; set; } = new();
    
        public List<Vote> Votes { get; set; } = new();
    }
}
