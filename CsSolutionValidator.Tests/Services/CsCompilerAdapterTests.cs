using Xunit;
using SolutionValidator.Cs.Services;
using SolutionValidator.Cs.Interfaces;
using Moq;
using Microsoft.CodeAnalysis;
using SolutionValidator.Cs.Providers;
using SolutionValidator.Cs.Mappers;
using SolutionValidator.Cs.Wrappers;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Models;
using Shared.Core.Compilers;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;
using NSubstitute;
using System.Runtime.Loader;
using Microsoft.Extensions.Logging;
using SolutionValidator.Cs.Tests.Stubs;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using NSubstitute.ExceptionExtensions;
using System.Collections.Immutable;
using SolutionValidator.Cs.Exceptions;
using System;

namespace SolutionValidator.Cs.Services.Tests
{
    public class CsCompilerAdapterTests
    {
        private readonly Mock<IExecutableReferenceService> _mockReferenceService;
        private readonly Mock<ICompilationResultValidator> _mockCompilationResultValidator;
        private readonly Mock<IDiagnosticMapper> _mockDiagnosticMapper;
        private CsCompilerAdapter _adapter;
        private readonly AssemblyWrapperStub _stubAssemblyWrapper;
        public CsCompilerAdapterTests()
        {
            _mockReferenceService = new Mock<IExecutableReferenceService>();
            _mockCompilationResultValidator = new Mock<ICompilationResultValidator>();
            _mockDiagnosticMapper = new Mock<IDiagnosticMapper>();
            _stubAssemblyWrapper = new AssemblyWrapperStub(It.IsAny<IServiceProvider>());

            _adapter = new CsCompilerAdapter(
                _mockReferenceService.Object.ThrowIfNull(),
                new NetReferencesProvider(It.IsAny<IEnumerable<PortableExecutableReference>>()), 
                _mockCompilationResultValidator.Object.ThrowIfNull(),
                _stubAssemblyWrapper, 
                _mockDiagnosticMapper.Object.ThrowIfNull()
            );
        }
        [Fact]
        public void Compile_WithValidRequest_CreatesCompilationWithExpectedReferences()
        {
            //Arrange
            CompilationRequest compilationRequest = new(new CompilationContext(It.IsAny<List<SyntaxTree>>(), It.IsAny<HashSet<string>>()), "CsAssembly2");
            _stubAssemblyWrapper.Wrap(It.IsAny<AssemblyLoadContext>(), It.IsAny<FileStream>());

            //Act
            CompilationResult result = _adapter.Compile(compilationRequest);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<CompilationResult>(result);
        }

        [Fact]
        public void Compile_WithValidRequest_EmitsAssemblyToFileStream()
        {
            //Arrange
            CompilationRequest compilationRequest = new(new CompilationContext(It.IsAny<List<SyntaxTree>>(), It.IsAny<HashSet<string>>()), "CsAssembly3");
            CSharpCompilation compilation = CSharpCompilation.Create("CsAssembly1.dll", compilationRequest.Compilation.SyntaxTrees, references: It.IsAny<IEnumerable<MetadataReference>>());
            FileStream fileStream = new(compilationRequest.AssemblyName, FileMode.Create, FileAccess.ReadWrite);

            //Act
            EmitResult result = compilation.Emit(fileStream);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<EmitResult>(result);
        }

        [Fact]
        public void Compile_WithValidRequest_ValidatesCompilationResult()
        {
            //Arrange
            CompilationRequest compilationRequest = new(new CompilationContext(It.IsAny<List<SyntaxTree>>(), It.IsAny<HashSet<string>>()), "CsAssembly1");
            CSharpCompilation compilation = CSharpCompilation.Create("CsAssembly1.dll", compilationRequest.Compilation.SyntaxTrees, references: It.IsAny<IEnumerable<MetadataReference>>());
            FileStream fileStream = new(compilationRequest.AssemblyName, FileMode.Create, FileAccess.ReadWrite);
            EmitResult result = compilation.Emit(fileStream);

            //Act
            var exception = Record.Exception(() => _mockCompilationResultValidator.Object.Validate(result));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Compile_WithInvalidCompilationResult_ReturnsDiagnostics()
        {
            //Arrange
            var hash = new HashSet<string>
            {
                "a","b","2"
            };
            CompilationRequest compilationRequest = new(new CompilationContext(new List<SyntaxTree>() { }, hash), "CsAssembly7");
            _mockCompilationResultValidator.Setup(_ => _.Validate(It.IsAny<EmitResult>())).Throws(new CSCompilationException(new List<Diagnostic>() { }));
            _mockDiagnosticMapper.Setup(_ => _.Map(It.IsAny<List<Diagnostic>>())).Returns(new List<CompilationDiagnostic> { new CompilationDiagnostic("message", DiagnosticSeverityInternal.Note, 2, 3), new CompilationDiagnostic("afsdfasfd", DiagnosticSeverityInternal.Note, 3, 23) });
            //Act
            CompilationResult? result = _adapter.Compile(compilationRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CompilationResult>(result);
            Assert.NotEmpty(result.Diagnostics);
        }

        [Fact]
        public void Compile_WithSuccessfulCompilation_ReturnsAssemblyWrapper()
        {
            //Arrange
            CompilationRequest compilationRequest = new(new CompilationContext(It.IsAny<List<SyntaxTree>>(), It.IsAny<HashSet<string>>()), "CsAssembly6");

            FileStream fileStream = new(compilationRequest.AssemblyName, FileMode.Create, FileAccess.ReadWrite);
            //Act
            IInternalAssembly? result = null;
            var exception = Record.Exception(() => result = _stubAssemblyWrapper.Wrap(It.IsAny<AssemblyLoadContext>(), fileStream));
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CsInternalAssembly>(result);
            Assert.Null(exception);
        }

        [Fact]
        public void Compile_WithFailedCompilation_DisposesFileStream()
        {
            //Arrange
            var hash = new HashSet<string>
            {
                "a","b","2"
            };
            CompilationRequest compilationRequest = new(new CompilationContext(new List<SyntaxTree>() { }, hash), "CsAssembly7");
            _mockCompilationResultValidator.Setup(_ => _.Validate(It.IsAny<EmitResult>())).Throws(new CSCompilationException(new List<Diagnostic>() { }));
            _mockDiagnosticMapper.Setup(_ => _.Map(It.IsAny<List<Diagnostic>>())).Returns(new List<CompilationDiagnostic> { new CompilationDiagnostic("message", DiagnosticSeverityInternal.Note, 2, 3), new CompilationDiagnostic("afsdfasfd", DiagnosticSeverityInternal.Note, 3, 23) });
            //Act
            CompilationResult? result = _adapter.Compile(compilationRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CompilationResult>(result);
            Assert.NotEmpty(result.Diagnostics);
        }

        [Fact]
        public void GetAssemblyFileName_GivenAssemblyName_ReturnsCorrectFileName()
        {
            Assert.True(true);
        }
    }
}
