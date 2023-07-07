namespace CodeProblemAssistant.Contract.Models
{
    public class CodeProblemVotePatchRequest
    {
        public Guid CodeProblemUUID { get; set; }

        public Guid UserReferenceUUID { get; set; }

        public bool UpVote { get; set; }
    }
}
