namespace CodeProblemAssistant.Contract.Models
{
    public class CodeProblemVotesQueryRequest
    {
        public Guid? CodeProblemUUID { get; set; }

        public Guid? UserReferenceUUID { get; set; }

        public int? Id { get; set; }

        public bool? UpVote { get; set; }
    }
}
