using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Core.Enums;

namespace Mongo.Data.Models
{
    public class CodeRunQueueMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid UUID { get; set; }

        public Guid UserReferenceUUID { get; set; }

        public Guid CodeProblemReferenceUUID { get; set; }

        public CodeLanguage CodeLanguageId { get; set; }

        public RunType RunTypeId { get; set; }

        public string SourceCode { get; set; } = string.Empty;
    }
}
