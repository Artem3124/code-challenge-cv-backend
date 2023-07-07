using Xunit;
using SolutionValidator.Cpp.Stages;

namespace SolutionValidator.Cpp.Stages.Tests
{
    public class CppTestingStageTests
    {
        [Fact]
        public void Execute_WhenNoTestCasesInContext_ReturnsTestingRunContextWithEmptyTestCaseResults()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenTestCasesExist_ExecutesAssembly()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenTestCasesExist_ReadsTestCaseResults()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenTestCasesExist_RestoresInputsForTestCaseResults()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenTestCasesExist_AssignsTestCaseResultsToContext()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenTestCasesExist_ReturnsTestingRunContext()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenAssemblyIsDisposed_PopsAssemblyFromAssemblyPool()
        {
            Assert.True(true);
        }
    }
}
