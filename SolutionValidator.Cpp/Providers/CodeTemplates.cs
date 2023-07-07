namespace SolutionValidator.Cpp.Providers
{
    internal static class CodeTemplates
    {
        public static string TestCaseClass = @"class TestCase
{
public:
    TestCase(int Id, {expectedType} Expected, {inputs})
    {
        this->Id = Id;
        this->Expected = Expected;
        {inputInitializations}
    }
    int Id;
    {expectedType} Expected;
    {inputFields}
};";

        public static string TestCaseInputInitialization = "this->{fieldName} = {fieldName};";

        public static string CallArg => "{instanceName}.{fieldName}";

        public static string TestCaseFactoryClass = @"class TestCaseFactory
{
public:

    static void Init(const std::list<TestCase>& testCases)
    {
        if (!Instance)
        {
            delete Instance;
        }
        Instance = new TestCaseFactory(testCases);
    }

    static {expectedType} ExecuteNext()
    {
        Solution solution;
        TestCase testCase = *Instance->Current;

        {expectedType} result = solution.{methodName}({testCaseInputs});

        Instance->Current++;

        return result;
    }

    static int Id()
    {
        return (*Instance->Current).Id;
    }

    static {expectedType} Expected()
    {
        return (*Instance->Current).Expected;
    }

private:
    std::list<TestCase> TestCases;
    std::list<TestCase>::iterator Current;
    static TestCaseFactory* Instance;

    TestCaseFactory(const std::list<TestCase>& testCases)
    {
        TestCases = testCases;
        Current = TestCases.begin();
    }
    ~TestCaseFactory()
    {
        if (!Instance)
        {
            delete Instance;
        }
    }
};
TestCaseFactory *TestCaseFactory::Instance = nullptr;
";

        public static string EntryPoint = @"int main(int argc, char *argv[])
{
    std::list<TestCase> testCases
    {
        {testCasesInitialization}
    };
    TestCaseFactory::Init(testCases);
    TestContext context;
    context.run(TestCaseFactory::ExecuteNext, TestCaseFactory::Expected, TestCaseFactory::Id, testCases.size());
}";

        public static string TestCaseInitialization = "TestCase({id}, {expected}, {inputs})";

        public static string SolutionTemplate = @"class Solution
{
public:
    {returnType} {methodName}({args})
    {

    }
};";

        public static string InternalInclude = "#include \"{0}\"";

        public static string ExteranlInclude = "#include <{0}>";

        public static string HeaderFileTemplate = @"#ifndef {fileName}
#define {fileName}
{content}
#endif";
    }
}
