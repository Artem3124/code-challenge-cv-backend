using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Mongo.Data.Models
{
    public class TestCaseSet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid UUID { get; set; }

        public List<TestCaseItem> TestCases { get; set; }
    }
}
