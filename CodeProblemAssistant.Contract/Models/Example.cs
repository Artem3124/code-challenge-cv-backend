namespace CodeProblemAssistant.Contract.Models
{
    public class Example
    {
        public string Input { get; set; }

        public string Output { get; set; }

        public Example(string input, string output)
        {
            Input = input;
            Output = output;
        }
    }
}
