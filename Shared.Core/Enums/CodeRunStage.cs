using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Shared.Core.Enums
{
    public enum CodeRunStage
    {
        Queued = 0,
        Compiling = 1,
        Testing = 2,
        Completed = 3,
    }
}
