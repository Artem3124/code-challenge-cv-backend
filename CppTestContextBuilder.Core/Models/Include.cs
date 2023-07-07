namespace CppTestContextBuilder.Core.Models
{
    public class Include
    {
        public string Name { get; init; }

        public string? ReferenceLocation { get; init; }

        public IncludeType Type { get; set; }

        public Include(string name, IncludeType type, string? referenceLocation = default)
        {
            Name = name;
            ReferenceLocation = referenceLocation;
            Type = type;
        }
    }

    public enum IncludeType
    {
        Internal = 0,
        External = 1,
    };
}
