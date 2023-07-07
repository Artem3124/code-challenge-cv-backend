namespace CodeProblemAssistant.Contract.Models
{
    public class TagCreateRequestBatch
    {
        public List<string> Names { get; set; }

        public Guid CodeProblemUUID { get; set; }

        public TagCreateRequestBatch(List<string> names, Guid codeProblemUUID)
        {
            Names = names;
            CodeProblemUUID = codeProblemUUID;
        }
    }
}
