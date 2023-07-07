using Shared.Core.Compilers;

namespace SolutionValidator.Python.Services
{
    internal class PythonSyntaxCheckResultParser
    {
        public List<CompilationDiagnostic> Parse(string value)
        {
            var compilationDiagnostics = new List<CompilationDiagnostic>();

            var lines = value.Split('\n');

            var diagnostic = CreateDiagnosticError();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("  File \""))
                {
                    var substring = lines[i].Split(',');
                    var linePos = substring.FirstOrDefault(s => s.Contains("line "));
                    if (linePos != null)
                    {
                        diagnostic.Line = int.Parse(linePos.Trim().Split(' ')[1]);
                    }
                }
                if (lines[i].Split(':')[0].Contains("Error"))
                {
                    diagnostic.Message = lines[i];
                    compilationDiagnostics.Add(diagnostic);
                    diagnostic = CreateDiagnosticError();
                }
                else if (lines[i].Contains('^'))
                {
                    diagnostic.Message = lines[i + 1];
                    compilationDiagnostics.Add(diagnostic);
                    diagnostic = CreateDiagnosticError();
                    i++;
                }
            }

            return compilationDiagnostics;
        }

        private CompilationDiagnostic CreateDiagnosticError() => new()
        {
            Column = 0,
            Line = 0,
            Severity = DiagnosticSeverityInternal.Error,
        };
    }
}
