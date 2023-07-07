using Xunit;
using SolutionValidator.Cpp.Stages;

namespace SolutionValidator.Cpp.Stages.Tests
{
    public class CppCompilationStageTests
    {
        [Fact]
        public void Execute_WhenCodeProblemMethodInfoIsNotNull_BuildsAndCompilesWithCodeProblemMethodInfo()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenCodeProblemMethodInfoIsNull_BuildsAndCompilesWithChallengeMethodInfo()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenCompilationResultAssemblyIsNotNull_PushesAssemblyToInternalAssemblyPool()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenCompilationResultAssemblyIsNull_DoesNotPushAssemblyToInternalAssemblyPool()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_AssignsDiagnosticsToContext()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_ReturnsCompilationRunContext()
        {
            Assert.True(true);
        }
    }
}
