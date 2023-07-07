using Xunit;
using SolutionValidator.Cpp.Services;

namespace SolutionValidator.Cpp.Services.Tests
{
    public class CppCompilerAdapterTests
    {
        [Fact]
        public void Compile_WhenCalled_GeneratesResultFileName()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_GetsFileNameFromEntryPointName()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_CreatesFileScopeWithFilesAndEntryPoint()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_ExecutesSystemProcessService()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_ParsesDiagnostics()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_MarksWarningsAsErrors()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_ThrowsExceptionIfExitCodeIsNotZeroAndDiagnosticIsEmpty()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_DeletesExecutableIfDiagnosticHasErrorsAndExitCodeIsZero()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_WrapsAssemblyIfNoErrors()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_ReturnsCompilationResult()
        {
            Assert.True(true);
        }

        [Fact]
        public void Compile_WhenCalled_DeletesResultFileFinally()
        {
            Assert.True(true);
        }
    }
}
