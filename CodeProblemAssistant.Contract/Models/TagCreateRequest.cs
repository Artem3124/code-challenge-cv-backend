namespace CodeProblemAssistant.Contract.Models
{
    public class TagCreateRequest
    {
        public string Name { get; set; }

        public int CodeProblemId { get; set; }

        public TagCreateRequest(string name, int codeProblemId)
        {
            Name = name;
            CodeProblemId = codeProblemId;
        }
    }
}
