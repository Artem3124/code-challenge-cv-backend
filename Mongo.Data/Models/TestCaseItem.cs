using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Mongo.Data.Models
{
    public class TestCaseItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        public int Id { get; set; }

        public string Expected { get; set; }

        public List<string> Input { get; set; }
    }
}
