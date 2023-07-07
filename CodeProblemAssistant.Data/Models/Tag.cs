using Shared.Core.Models;

namespace CodeProblemAssistant.Data.Models
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public List<CodeProblem> CodeProblems { get; set; } = new();
    }
}
