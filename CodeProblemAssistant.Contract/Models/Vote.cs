namespace CodeProblemAssistant.Contract.Models
{
    public class Vote
    {
        public Guid CodeProblemUUID { get; set; }

        public bool UpVote { get; set; }
    }
}
