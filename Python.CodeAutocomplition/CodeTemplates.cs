namespace Python.CodeAutocomplition
{
    internal static class CodeTemplates
    {
        public static string SolutionTemplate => "def {methodName}({inpust}):\n\t";

        public static string TestMethodDefinition => "class TestMethod(unittest.TestCase):\n";

        public static string Main => "if __name__ == '__main__':\n\t";

        public static string UnitTestMainContent => "unittest.main()";

        public static string TestCaseDefinition => "def testcase_{0}(self):\n";

        public static string TestCaseContent => "self.assertEqual({0}.{1}({2}), {3})";

        public static string Import => "import {importName}";
    }
}