namespace CppTestContextBuilder.Core.Models
{
    public class CppCompilationContext
    {
        public List<CppFile> Files { get; set; }

        public HashSet<string> References { get; set; }

        public CppFile EntryPointFile { get; set; }

        public CppCompilationContext(List<CppFile> files, CppFile entryPointFile, HashSet<string>? references = default)
        {
            if (files is null || files.Count == 0)
            {
                throw new ArgumentNullException(nameof(files));
            }
            Files = files;
            References = references ?? new();
            EntryPointFile = entryPointFile;
        }
    }
}
