using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryProject.Infrastructure.Mongo.ReadModels
{
    public class BookReadModelDel
    {
        [BsonId]
        public Guid BookId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("author")]
        public string Author { get; set; } = string.Empty;

        [BsonElement("yearPublication")]
        public int YearPublication { get; set; }

        [BsonElement("availableQuantity")]
        public int AvailableQuantity { get; set; }
    }
}
