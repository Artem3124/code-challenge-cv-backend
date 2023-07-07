namespace FileScopeProvider.Models
{
    public class SystemFile
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public SystemFile(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}