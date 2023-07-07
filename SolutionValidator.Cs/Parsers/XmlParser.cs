using System.Text;
using System.Xml.Serialization;

namespace SolutionValidator.Cs.Parsers
{
    public class XmlParser
    {
        public T? ParseFile<T>(string fileName)
        {
            var serializer = new XmlSerializer(typeof(T));

            using var fileStream = File.OpenRead(fileName);

            var result = serializer.Deserialize(fileStream);

            return Parse<T>(result);
        }

        public T? ParseString<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            var result = serializer.Deserialize(memoryStream);

            return Parse<T>(result);
        }

        private T? Parse<T>(object? obj)
        {
            if (obj == null)
            {
                return default;
            }

            return (T)obj;
        }
    }
}
