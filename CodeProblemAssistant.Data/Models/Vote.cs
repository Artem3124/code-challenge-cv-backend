namespace CodeProblemAssistant.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public Guid UserReferenceUUID { get; set; }

        public int CodeProblemId { get; set; }

        public Guid CodeProblemUUID { get; set; }

        public CodeProblem CodeProblem { get; set; }

        public bool UpVote { get; set; }
    }
}
