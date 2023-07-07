namespace AccountManager.Api.Interfaces
{
    public interface IHashService
    {
        string Hash(string value);
        bool Verify(string value, string hashValue);
    }
}