using Shared.Core.Interfaces;

namespace Python.TestContextBuilder.Services
{
    public class StringFileNameGenerator : IFileNameGenerator
    {
        private readonly Random _random;

        private readonly int _nameLength = 16;

        private readonly string _charSet = "abcdefghijklmnopqrstuvwxyz";

        public StringFileNameGenerator(Random random)
        {
            _random = random;
        }

        public string Generate(string? prefix = null, string? postfix = null) => new(
            $"{prefix}{string.Join(string.Empty, Enumerable.Repeat(_charSet, _nameLength).Select(s => s[_random.Next(s.Length)]))}{postfix}"
        );
    }
}
