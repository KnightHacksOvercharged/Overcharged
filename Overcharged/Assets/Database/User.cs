using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Assets.Database
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("displayName")]
        public string DisplayName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("bestScore")]
        public int BestScore { get; set; }
    }
}