namespace FileScopeProvider.Interfaces
{
    public interface IFilePurger
    {
        bool Delete(string path);
    }
}