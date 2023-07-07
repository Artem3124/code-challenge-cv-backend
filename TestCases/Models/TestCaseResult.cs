namespace TestCases.Models;

public class TestCaseResult
{
    public int Id { get; set; }
    public string Actual { get; set; }

    public string Expected { get; set; }

    public string? Message { get; set; }

    public string Inputs { get; set; }

    public string Result { get; set; }

    public TestCaseResult() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0)
    {

    }

    public TestCaseResult(string inputs, string expected, string actual, string result, string? message, int id)
    {
        Actual = actual;
        Inputs = inputs;
        Expected = expected;
        Result = result;
        Message = message;
        Id = id;
    }
}
