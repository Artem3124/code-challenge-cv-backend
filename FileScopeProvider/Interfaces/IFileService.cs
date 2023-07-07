namespace FileScopeProvider.Interfaces
{
    public interface IFileService
    {
        string Read(string fileName);
        void Write(string content, string fileName);
    }
}