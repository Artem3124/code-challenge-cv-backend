using Shared.Core.Interfaces;

namespace Shared.Core.Services
{
    public class FileNameGenerator : IFileNameGenerator
    {
        public string Generate(string? prefix = default, string? postfix = default) =>
            $"{GetPrefix(prefix)}{Guid.NewGuid()}{GetPostfix(postfix)}".Replace('-', '_');

        private string GetPrefix(string? value)
            => value == default ? string.Empty : $"{value}_";

        private string GetPostfix(string? value)
            => value == default ? string.Empty : $"_{value}";
    }
}
