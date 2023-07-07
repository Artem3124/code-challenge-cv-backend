using FileScopeProvider.Models;

namespace FileScopeProvider.Interfaces
{
    public interface IFileScopeProvider
    {
        IFileScope Create(List<SystemFile> files);
    }
}