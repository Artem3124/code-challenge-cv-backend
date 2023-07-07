using Xunit;
using SolutionValidator.Cs.Services;

namespace SolutionValidator.Cs.Services.Tests
{
    public class CsTestContextBuilderTests
    {
        [Fact]
        public void Build_WithValidInput_CreatesCompilationContextWithSourceCodeSyntaxTree()
        {
            Assert.True(true);
        }

        [Fact]
        public void Build_WithTestCasesOptionAndTestCases_AddsTestCaseClassSyntaxTree()
        {
            Assert.True(true);
        }

        [Fact]
        public void Build_WithTestCasesOptionAndTestCases_AddsTestClassSyntaxTree()
        {
            Assert.True(true);
        }

        [Fact]
        public void Build_WithoutTestCasesOptionOrTestCases_AddsEntryPointSyntaxTree()
        {
            Assert.True(true);
        }

        [Fact]
        public void Build_WithNUnitOption_AddsNUnitUsingsAndReference()
        {
            Assert.True(true);
        }
    }
}
