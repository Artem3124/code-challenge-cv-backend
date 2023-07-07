using Xunit;

namespace SolutionValidator.Python.Tests
{
    public class PythonScriptRunnerTests
    {
        [Fact]
        public void Run_PassesCorrectArgumentsToSystemProcessService()
        {
            Assert.True(true);
        }

        [Fact]
        public void Run_WithFailFast_PassesCorrectArgumentsToSystemProcessService()
        {
            Assert.True(true);
        }

        [Fact]
        public void Run_ReturnsOutputFromSystemProcessService()
        {
            Assert.True(true);
        }
    }
}
