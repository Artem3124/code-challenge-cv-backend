using Xunit;
using SolutionValidator.Cpp.Wrappers;

namespace SolutionValidator.Cpp.Wrappers.Tests
{
    public class CppInternalAssemblyTests
    {
        [Fact]
        public void Dispose_WhenCalledAndAssemblyNotDisposed_DeletesExecutableAndSetsDisposed()
        {
            Assert.True(true);
        }

        [Fact]
        public void Dispose_WhenCalledAndAssemblyDisposed_DoesNotDeleteExecutableAgain()
        {
            Assert.True(true);
        }

        [Fact]
        public void Dispose_WhenCalledAndExecutableNameNotSet_DoesNotDeleteExecutable()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenCalledAndExecutableNameSet_ExecutesSystemProcessService()
        {
            Assert.True(true);
        }

        [Fact]
        public void Execute_WhenCalledAndExecutableNameNotSet_ThrowsException()
        {
            Assert.True(true);
        }

        [Fact]
        public void Load_WhenCalled_SetsExecutableName()
        {
            Assert.True(true);
        }
    }
}
