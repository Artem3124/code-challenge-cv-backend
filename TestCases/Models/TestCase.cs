namespace TestCases.Models
{
    public class TestCase
    {
        public int Id { get; set; }

        public List<string> Input { get; set; }

        public string Expected { get; set; }

        public TestCase()
        {
            Input = new List<string>();
            Expected = string.Empty;
        }

        public TestCase(List<string> input, string expected, int id)
        {
            Input = input ?? new List<string>();
            Expected = expected ?? string.Empty;
            Id = id;
        }
    }
}
