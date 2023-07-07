using Newtonsoft.Json;

namespace Shared.Core.Extensions
{
    public static class MetadataEntryNames
    {
        public static string Duration = "Duration";

        public static string FailedTestCase = "FailedTestCase";

        public static string CompilationErrors = "CompilationErrros";
    }

    public static class MetadataExtensions
    {
        public static bool AddMetadataEntry<T>(this Dictionary<string, string> metadata, T obj, string objName)
        {
            return metadata.TryAdd(objName, JsonConvert.SerializeObject(obj));
        }
    }
}
