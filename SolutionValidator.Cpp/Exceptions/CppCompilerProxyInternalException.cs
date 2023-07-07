namespace SolutionValidator.Cpp.Exceptions
{
    public class CppCompilerProxyInternalException : Exception
    {
        public int Code { get; init; }

        public CppCompilerProxyInternalException(int code) : base($"Compilation returned code {code} but no diagnostics were found. Check CodeRunManager.Api/log file.")
        {
            Code = code;
        }
    }
}
