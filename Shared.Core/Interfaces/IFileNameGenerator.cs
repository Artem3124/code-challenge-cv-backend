namespace Shared.Core.Interfaces
{
    public interface IFileNameGenerator
    {
        string Generate(string? prefix = default, string? postfix = default);
    }
}