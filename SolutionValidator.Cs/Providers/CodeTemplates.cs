namespace SolutionValidator.Cs.Providers
{
    internal static class CodeTemplates
    {
        public static string EntryPoint => @"
        public class Program
        {
            public static void Main()
            {
                {0}
            }
        }
        ";

        public static string Using => "using {0};\n\r";

        public static string GlobalUsing => "global using {0};\n\r";

        public static string Namespace => "namespace {0};";

        public static string NUnitTestCase => "yield return new TestCaseData({0}).Returns({1});\n\r";

        public static string NUnitLite => "new AutoRun().Execute(null);";

        public static string NUnitTestCaseFactory => @"{namespaceName}
        public static class TestCasesFactory
        {
            public static IEnumerable TestCases
            {
                get
                {
                    {testCasesSourceCode}
                }
            }
        }
        ";

        public static string NUnitTestMethodAttributes => "[Test, TestCaseSource(typeof({0}), nameof({0}.{1}))]\n\r";

        public static string NUnitTestFixture => "[TestFixture]\n\r";

        public static string SolutionTemplate =>
@"public class Solution
{
    public {returnType} {methodName}({args})
    {
        
    }
}";

        public static string TestSubject => "{namespaceName} public class Program { public static void Main() { new AutoRun().Execute(null); } } [TestFixture] public class TestSubject { [TestCaseSource(typeof(TestCasesFactory), nameof(TestCasesFactory.TestCases))] public {returnType} TestMethod({args}) { return new Solution().{methodName}({callArgs}); } }";
    }
}
