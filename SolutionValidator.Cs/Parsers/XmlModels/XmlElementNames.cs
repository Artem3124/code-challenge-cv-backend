namespace SolutionValidator.Cs.Parsers.XmlModels
{
    public static class TestRunElementNames
    {
        public const string Root = "test-run";

        public const string Id = "id";
    }

    public static class TestSuiteElementNames
    {
        public const string Root = "test-suite";

        public const string Id = "id";
    }

    public static class FailureElementNames
    {
        public const string Root = "failure";

        public const string Message = "message";
    }

    public static class TestCaseElementNames
    {
        public const string Root = "test-case";

        public const string Name = "fullname";

        public const string Id = "id";

        public const string Result = "result";

        public const string StartTime = "start-time";

        public const string EndTime = "end-time";

        public const string DurationSeconds = "duration";

        public const string ClassName = "classname";

        public const string MethodName = "methodname";
    }
}
