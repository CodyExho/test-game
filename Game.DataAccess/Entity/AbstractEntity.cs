using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Game.DataAccess.Entity
{
    public abstract class AbstractEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
